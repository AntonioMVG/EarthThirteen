using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public LineRenderer lineRenderer;
    private Vector3[] positions = new Vector3[2];

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out hit, 100f))
        {
            positions[0] = transform.position;
            positions[1] = hit.transform.position;
            Rigidbody rb;
            rb = hit.collider.gameObject.GetComponent<Rigidbody>();
        }
            
    }
}
