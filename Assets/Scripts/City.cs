using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ZSerializer;

public class City : PersistentMonoBehaviour
{
    [Header("Day")]
    [SerializeField] private GameObject sun;
    public float curDayTime;
    public float dayTime;
    public float multiplier;

    [Header("Stats")]
    public int day;
    public int money;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int polution;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJobs;

    [Header("HUD")]
    public TextMeshProUGUI dayTxt;
    public TextMeshProUGUI moneyTxt;
    public TextMeshProUGUI populationTxt;
    public TextMeshProUGUI jobsTxt;
    public TextMeshProUGUI foodTxt;
    public TextMeshProUGUI polutionTxt;
    public TextMeshProUGUI hourTxt;
    public TextMeshProUGUI multiplierTxt;

    public List<Building> buildings = new List<Building>();

    [Header("Buildings Container")]
    public GameObject containers;
    public GameObject housesContainer;
    public GameObject factoriesContainer;
    public GameObject farmsContainer;
    public GameObject roadsContainer;
    public GameObject treesContainer;
    public GameObject pipesContainer;

    [Header("HUD Buttons")]
    public Button houseBt;
    public Button factoryBt;

    [Header("End Game")]
    [SerializeField] private GameObject endGameInfo;

    [Header("Counters")]
    public int carCount;
    public int humanCount;
    
    public static City instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        endGameInfo.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(!IsLoading)
        {
            if (curPopulation <= 50)
            {
                houseBt.transform.GetComponent<Button>().interactable = false;
                factoryBt.transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                houseBt.transform.GetComponent<Button>().interactable = true;
                factoryBt.transform.GetComponent<Button>().interactable = true;
            }

            UpdateStatsText();
            DayCicle();
        }
    }

    private void DayCicle()
    {
        // Calculate the current hour of day, based on 24h, depending on the DayTime
        curDayTime += Time.deltaTime * multiplier;
        float hours = (float)(curDayTime / dayTime) * 24;
        float minutes = (hours - Mathf.FloorToInt(hours)) * 60;
        hourTxt.text = (Mathf.FloorToInt(hours).ToString("00") + ":" + Mathf.FloorToInt(minutes).ToString("00"));

        // Turn of/off the lights depending on the time of day
        if(curDayTime >= (dayTime /2))
            TurnOnLights();
        else
            TurnOffLights();

        if (curDayTime >= dayTime)
        {
            curDayTime = 0;
            EndTurn();
        }

        // Rotate the Light with the current day time / day time multiplied with 360 degrees
        sun.transform.rotation = Quaternion.Euler((curDayTime / dayTime) * 360, 0f, 0f);

        // Move the Skybox with the current time
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * multiplier);
    }

    // Called when we place down a bulding
    public void OnPlaceBuilding(Building building)
    {
        if(!building.CompareTag("Road"))
            buildings.Add(building);

        switch (building.tag)
        {
            case "HouseAMVG":
                building.transform.SetParent(housesContainer.transform);
                break;
            case "House":
                building.transform.SetParent(housesContainer.transform);
                break;
            case "FactoryAMVG":
                building.transform.SetParent(factoriesContainer.transform);
                break;
            case "Factory":
                building.transform.SetParent(factoriesContainer.transform);
                break;
            case "Farm":
                building.transform.SetParent(farmsContainer.transform);
                break;
            case "Road":
                building.transform.SetParent(roadsContainer.transform);
                break;
            case "Tree":
                building.transform.SetParent(treesContainer.transform);
                break;
            case "Pipe":
                building.transform.SetParent(pipesContainer.transform);
                break;
        }

        money -= building.preset.cost;
        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;
        curFood += building.preset.food;
        polution += building.preset.polution;

        UpdateStatsText();
    }

    // Called when we bulldoze a building
    public void OnRemoveBuilding(Building building)
    {
        buildings.Remove(building);

        money += building.preset.cost / 3;
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;

        Destroy(building.gameObject);

        UpdateStatsText();
    }

    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CalculatePolution();
        UpdateStatsText();
    }

    private void CalculateMoney()
    {
        money += curJobs * incomePerJobs;
        foreach (Building building in buildings)
            money -= building.preset.costPerTurn;

        if(money < 0)
            endGameInfo.SetActive(true);
    }

    private void CalculatePopulation()
    {
        // TODO: Mejorar este sistema, falla mucho
        if(curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4), maxPopulation);
        }
        else if(curFood < curPopulation)
        {
            curPopulation = curFood;
        }
    }

    private void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }

    private void CalculateFood()
    {
        curFood = 0;
        foreach(Building building in buildings)
            curFood += building.preset.food;
    }

    private void CalculatePolution()
    {
        polution = 0;
        foreach (Building building in buildings)
            polution += building.preset.polution;
    }

    public void UpdateStatsText()
    {
        dayTxt.text = day.ToString();
        moneyTxt.text = money.ToString();
        populationTxt.text = curPopulation.ToString() + "/" + maxPopulation.ToString();
        jobsTxt.text = curJobs.ToString() + "/" + maxJobs.ToString();
        foodTxt.text = curFood.ToString();
        polutionTxt.text = polution.ToString();
        multiplierTxt.text = "x" + multiplier.ToString();
    }

    public void DayAvanced()
    {
        if(multiplier <= 16)
        {
            multiplier *= 2;
        }
    }

    public void DaySlow()
    {
        if(multiplier > 1)
        {
            multiplier /= 2;
        }
    }

    public void TurnOnLights()
    {
        foreach(Building building in buildings)
        {
            if (building != null)
            {
                for (int i = 1; i < building.transform.childCount; i++)
                {
                    building.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    public void TurnOffLights()
    {
        foreach(Building building in buildings)
        {
            if(building != null)
            {
                for (int i = 1; i < building.transform.childCount; i++)
                {
                    building.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    public override void OnPostLoad()
    {
        base.OnPostLoad();
        dayTxt = ReferenceHolder.instance.dayTxt;
        moneyTxt = ReferenceHolder.instance.moneyTxt;
        populationTxt = ReferenceHolder.instance.populationTxt;
        jobsTxt = ReferenceHolder.instance.jobsTxt;
        foodTxt = ReferenceHolder.instance.foodTxt;
        polutionTxt = ReferenceHolder.instance.polutionTxt;
        hourTxt = ReferenceHolder.instance.hourTxt;
        multiplierTxt = ReferenceHolder.instance.multiplierTxt;

        houseBt = ReferenceHolder.instance.houseBt;
        factoryBt = ReferenceHolder.instance.factoryBt;

        sun = ReferenceHolder.instance.sun;
        endGameInfo = ReferenceHolder.instance.endGameInfo;

        instance = this;
        GetComponent<BuildingPlacement>().surface.BuildNavMesh();
    }
}
