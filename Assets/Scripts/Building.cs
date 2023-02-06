using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using ZSerializer;

public class Building : PersistentMonoBehaviour
{
    [SerializeField] private GameObject car;

    public BuildingPreset preset;
    public bool isNearRoad = false;

    private void Start()
    {
       if(car != null) 
            StartCoroutine(SpawnCar());
    }

    private void FixedUpdate()
    {
        if(!isNearRoad)
        {
            for(int i = 0; i < 4; i++)
            {
                Vector3 pos = transform.position;
                switch(i)
                {
                    case 0:
                        pos.x += 1;
                        break;
                    case 1:
                        pos.x -= 1;
                        break;
                    case 2:
                        pos.z += 1;
                        break;
                    case 3:
                        pos.z -= 1;
                        break;
                }
                List<Collider> colliders = Physics.OverlapBox(pos, transform.localScale / 2f).ToList();

                if(colliders.Where<Collider>(x => x.CompareTag("Road")).Count() > 0)
                    isNearRoad = true;
            }
        }
    }

    private IEnumerator SpawnCar()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            if(isNearRoad)
            {
                List<Building> buildings = new List<Building>();
                buildings.AddRange(City.instance.buildings);
                buildings.Remove(this);
                Vector3 position = buildings[Mathf.FloorToInt(Random.Range(0f, (int)buildings.Count))].transform.position;
                Instantiate(car, transform.position, Quaternion.identity).GetComponent<NavMeshAgent>().SetDestination(position);
            }
        }
    }
}
