using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{
    [Header("Day")]
    public GameObject sun;
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
    public GameObject housesContainer;
    public GameObject factoriesContainer;
    public GameObject farmsContainer;
    public GameObject roadsContainer;
    public GameObject treesContainer;
    public GameObject pipesContainer;

    public static City instance;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        UpdateStatsText();
        DayCicle();
    }

    private void DayCicle()
    {
        curDayTime += Time.deltaTime * multiplier;

        float hours = (float)(curDayTime / dayTime) * 24;
        float minutes = (hours - Mathf.FloorToInt(hours)) * 60;

        hourTxt.text = (Mathf.FloorToInt(hours).ToString("00") + ":" + Mathf.FloorToInt(minutes).ToString("00"));

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
    public void OnPlaceBulding(Building building)
    {
        buildings.Add(building);

        switch (building.tag)
        {
            case "House":
                building.transform.SetParent(housesContainer.transform);
                break;
            case "HouseAMVG":
                building.transform.SetParent(housesContainer.transform);
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
    }

    private void CalculatePopulation()
    {
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

    private void UpdateStatsText()
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
}
