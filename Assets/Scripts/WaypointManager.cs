using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private GameObject waypointMarker;
    [SerializeField] private List<Vector3> waypoints = new List<Vector3>();

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AddWaypoint(Vector3 road)
    {
        if(!waypoints.Contains(road))
        {
            waypoints.Add(road);
            Instantiate(waypointMarker, road, Quaternion.identity);
        }
           
    }

    public void RemoveWaypoint(Vector3 road)
    {
        if (waypoints.Contains(road))
        {
            waypoints.Remove(road);
            List<Collider> cols = Physics.OverlapBox(road, waypointMarker.transform.localScale / 2f, Quaternion.identity, 1 << 7).ToList();
            if(cols.Count > 0)
                Destroy(cols[0].gameObject);
        }
            
    }

    private float GetDistance(Vector3 nodeA, Vector3 nodeB)
    {
        return (nodeA - nodeB).sqrMagnitude;
    }
}
