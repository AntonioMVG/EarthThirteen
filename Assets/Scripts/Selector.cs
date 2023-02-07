using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera cam;

    public static Selector instance;

    [SerializeField] private LayerMask buildingMask;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Returns the tile that the mouse is hovering over
    public Vector3 GetCurTilePosition()
    {
        // Return if we have hovering over UI
        if (EventSystem.current.IsPointerOverGameObject())
            return new Vector3(0, -99, 9);

        // Create the plane, ray and out distance
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0.0f;

        if(plane.Raycast(ray, out rayOut))
        {
            // Get the position at which we intersected the plane
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);

            // Round that up to the nearest full number
            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0f, Mathf.CeilToInt(newPos.z));

            return newPos;
        }

        return new Vector3(0, -99, 9);
    }

    public Building GetBuildingInfo()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject.GetComponent<Building>();
        }

        return null;
    }

    public bool IsOccupied()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingMask | 1 << 7))
        {
            return true;
        }
        return false;
    }
}
