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

    [Header("Stats")]
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJobs;

    public TextMeshProUGUI statsText;

    public List<Building> buildings = new List<Building>();

    public static City instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateStatsText();
    }

    private void FixedUpdate()
    {
        DayCicle();
    }

    private void DayCicle()
    {
        curDayTime += Time.deltaTime;
        if (curDayTime >= dayTime)
        {
            curDayTime = 0;
            EndTurn();
        }
        sun.transform.rotation = Quaternion.Euler((curDayTime / dayTime) * 360, 0f, 0f);
    }

    // Called when we place down a bulding
    public void OnPlaceBulding(Building building)
    {
        buildings.Add(building);

        money -= building.preset.cost;
        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;

        UpdateStatsText();
    }

    // Called when we bulldoze a building
    public void OnRemoveBuilding(Building building)
    {
        buildings.Remove(building);

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

    private void UpdateStatsText()
    {
        statsText.text = string.Format("Day: {0} Money: {1} Pop;{2}/{3} Jobs: {4}/{5} Food: {6} ",
            new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });
    }
}
