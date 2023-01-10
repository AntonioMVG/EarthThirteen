using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;

    private float indicatorUpdateRate = 0.05f; // Seconds
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;
    public GameObject placementHouse;
    public GameObject placementFactory;
    public GameObject placementFarm;
    public GameObject placementRoad;
    public GameObject placementTree;

    // Called when we press a building UI button
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        // TODO: Make sure we have enough money

        currentlyPlacing = true;
        curBuildingPreset = preset;
        //placementIndicator.SetActive(true);
        if (preset.prefab.name == "BuildingHouse")
        {
            placementHouse.SetActive(true);
            placementFactory.SetActive(false);
            placementFarm.SetActive(false);
            placementRoad.SetActive(false);
            placementTree.SetActive(false);
        }
        else if (preset.prefab.name == "BuildingFactory")
        {
            placementHouse.SetActive(false);
            placementFactory.SetActive(true);
            placementFarm.SetActive(false);
            placementRoad.SetActive(false);
            placementTree.SetActive(false);
        }
        else if (preset.prefab.name == "BuildingFarm")
        {
            placementHouse.SetActive(false);
            placementFactory.SetActive(false);
            placementFarm.SetActive(true);
            placementRoad.SetActive(false);
            placementTree.SetActive(false);
        }
        else if (preset.prefab.name == "BuildingRoad")
        {
            placementHouse.SetActive(false);
            placementFactory.SetActive(false);
            placementFarm.SetActive(false);
            placementRoad.SetActive(true);
            placementTree.SetActive(false);
        }
        else if (preset.prefab.name == "BuildingTrees")
        {
            placementHouse.SetActive(false);
            placementFactory.SetActive(false);
            placementFarm.SetActive(false);
            placementRoad.SetActive(false);
            placementTree.SetActive(true);
        }
    }

    // Called when we press down a building or press Esc
    private void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        currentlyBulldozering = false;
        //placementIndicator.SetActive(false);
        placementHouse.SetActive(false);
        placementFactory.SetActive(false);
        placementFarm.SetActive(false);
        placementRoad.SetActive(false);
        placementTree.SetActive(false);
    }

    // Turn Bulldozer on/off
    public void ToggleBulldozer()
    {
        currentlyBulldozering = !currentlyBulldozering;
        bulldozerIndicator.SetActive(currentlyBulldozering);
    }

    private void Update()
    {
        // Cancel building placement
        if(Input.GetKeyDown(KeyCode.Escape))
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
        if (Input.GetMouseButtonDown(0) && currentlyPlacing)
            PlaceBuilding();
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)
            Bulldoze();
    }

    // Places down the currently selected building
    private void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);

        City.instance.OnPlaceBulding(buildingObj.GetComponent<Building>());

        if (!curBuildingPreset.prefab.tag.Equals("Road") && !curBuildingPreset.prefab.tag.Equals("Trees"))
            CancelBuildingPlacement();
    }

    // Delete the currently selected building
    private void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == curIndicatorPos);

        if (buildingToDestroy != null)
            City.instance.OnRemoveBuilding(buildingToDestroy);
    }
}
