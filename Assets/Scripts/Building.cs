using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public BuildingPreset preset;

    [SerializeField] private GameObject car;

    private void Start()
    {
       if(car != null) 
            StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            List<Building> buildings = new List<Building>();
            buildings.AddRange(City.instance.buildings);
            buildings.Remove(this);
            Vector3 position = buildings[Mathf.FloorToInt(Random.Range(0f, (int) buildings.Count))].transform.position;
            Instantiate(car, transform.position, Quaternion.identity).GetComponent<NavMeshAgent>().SetDestination(position);
        }
    }
}
