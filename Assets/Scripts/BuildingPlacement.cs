using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;

    private float indicatorUpdateRate = 0.05f; // Seconds
    private float lastUpdateTime;

    private Vector3 curIndicatorPos;

    [Header("Placements")]
    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;
    public GameObject placementHouse;
    public GameObject placementHouseAMVG;
    public GameObject placementFactory;
    public GameObject placementFarm;
    public GameObject placementRoad;
    public GameObject placementCurveRoad;
    public GameObject placementTree;
    public GameObject placementPipe;

    [Header("Particles")]
    public GameObject dustParticle;
    public GameObject explosionParticle;

    [Header("Building Info")]
    public TextMeshProUGUI nameInfo;
    public TextMeshProUGUI costInfo;
    public TextMeshProUGUI costPerTurnInfo;
    public TextMeshProUGUI populationInfo;
    public TextMeshProUGUI jobsInfo;
    public TextMeshProUGUI foodInfo;
    public TextMeshProUGUI polutionInfo;

    [Header("Panels Info")]
    public GameObject buildingInfo;
    public GameObject enoughMoneyInfo;

    // Called when we press a building UI button
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        // If you don't have enough money to build, active an informative panel
        if (City.instance.money < preset.cost)
        {
            currentlyPlacing = false;
            enoughMoneyInfo.SetActive(true);
        }
        else
        {
            currentlyPlacing = true;
            curBuildingPreset = preset;

            switch (preset.prefab.name)
            {
                case "BuildingHouse":
                    placementHouse.SetActive(true);
                    placementHouseAMVG.SetActive(false);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "HouseAMVG":
                    placementHouse.SetActive(false);
                    placementHouseAMVG.SetActive(true);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "BuildingFactory":
                    placementHouse.SetActive(false);
                    placementHouseAMVG.SetActive(false);
                    placementFactory.SetActive(true);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "BuildingFarm":
                    placementHouse.SetActive(false);
                    placementHouseAMVG.SetActive(false);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(true);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "BuildingRoad":
                    placementHouse.SetActive(false);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(true);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "BuildingCurveRoad":
                    placementHouse.SetActive(false);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(true);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "BuildingTree":
                    placementHouse.SetActive(false);
                    placementHouseAMVG.SetActive(false);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(true);
                    placementPipe.SetActive(false);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
                case "BuildingPipe":
                    placementHouse.SetActive(false);
                    placementHouseAMVG.SetActive(false);
                    placementFactory.SetActive(false);
                    placementFarm.SetActive(false);
                    placementRoad.SetActive(false);
                    placementCurveRoad.SetActive(false);
                    placementTree.SetActive(false);
                    placementPipe.SetActive(true);
                    buildingInfo.SetActive(false);
                    enoughMoneyInfo.SetActive(false);
                    break;
            }
        }
    }

    // Called when we press down a building or press Esc
    private void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        currentlyBulldozering = false;
        placementHouse.SetActive(false);
        placementHouseAMVG.SetActive(false);
        placementFactory.SetActive(false);
        placementFarm.SetActive(false);
        placementRoad.SetActive(false);
        placementCurveRoad.SetActive(false);
        placementTree.SetActive(false);
        placementPipe.SetActive(false);
        bulldozerIndicator.SetActive(false);
        buildingInfo.SetActive(false);
        enoughMoneyInfo.SetActive(false);
    }

    // Turn Bulldozer on/off
    public void ToggleBulldozer()
    {
        currentlyBulldozering = !currentlyBulldozering;
        bulldozerIndicator.SetActive(currentlyBulldozering);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            placementIndicator.transform.Rotate(0, 90, 0);

        // Cancel building placement
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();

        // Called every 0.05 seconds
        if(Time.time - lastUpdateTime > indicatorUpdateRate)
        {
            lastUpdateTime = Time.time;

            // Get the currently selected tile position
            curIndicatorPos = Selector.instance.GetCurTilePosition();

            // Move the placement/bulldozer indicator to the selected tile
            if (currentlyPlacing)
                placementIndicator.transform.position = curIndicatorPos;
            else if (currentlyBulldozering)
                bulldozerIndicator.transform.position = curIndicatorPos;
        }

        // Called when we press left mouse button
        Vector3 dummyPos = new Vector3(0, -99, 9);
        if (Input.GetMouseButtonDown(0) && currentlyPlacing)
        {
            // If pressing the HUD when have a building to place, prevents being sent to the Dummy position
            if (placementIndicator.transform.position != dummyPos && !Selector.instance.IsOccupied())
            {
                placementIndicator.GetComponent<AudioSource>().Play();
                GameObject particles = Instantiate(dustParticle, placementIndicator.transform.position, Quaternion.identity);
                Destroy(particles, 1f);
                PlaceBuilding();
            }
        }
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)
        {
            bulldozerIndicator.GetComponent<AudioSource>().Play();
            GameObject particles = Instantiate(explosionParticle, bulldozerIndicator.transform.position, Quaternion.identity);
            Destroy(particles, 1f);
            Bulldoze();
        }
        else if (Input.GetMouseButtonDown(0) && curIndicatorPos != dummyPos)
        {
            buildingInfo.SetActive(true);
            ShowBuildingInfo(Selector.instance.GetBuildingInfo());
        }
    }

    // Places down the currently selected building
    private void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, placementIndicator.transform.rotation);
        
        City.instance.OnPlaceBulding(buildingObj.GetComponent<Building>());
        
        CancelBuildingPlacement();
    }

    // Delete the currently selected building
    private void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == curIndicatorPos);

        if (buildingToDestroy != null)
            City.instance.OnRemoveBuilding(buildingToDestroy);
    }

    public void ShowBuildingInfo(Building building)
    {
        if (building == null)
        {
            buildingInfo.SetActive(false);
            return;
        }
        else
        {    
            nameInfo.text = "Type: " + building.preset.name.ToString();
            costInfo.text = "Cost: " + building.preset.cost.ToString();
            costPerTurnInfo.text = "Per day: " + building.preset.costPerTurn.ToString();
            populationInfo.text = "Population: " + building.preset.population.ToString();
            jobsInfo.text = "Jobs: " + building.preset.jobs.ToString();
            foodInfo.text = "Food: " + building.preset.food.ToString();
            polutionInfo.text = "Polution: " + building.preset.polution.ToString();
        }
    }

    public void CloseTab()
    {
        buildingInfo.SetActive(false);
        enoughMoneyInfo.SetActive(false);
    }
}
