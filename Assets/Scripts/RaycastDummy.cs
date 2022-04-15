using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDummy : MonoBehaviour
{
    public bool hittingEdge = false;
    public bool onTerrain = false;

    [SerializeField] private bool rightSide;

    [SerializeField] private float distance = 0.2f;

    private void Start()
    {
        if (rightSide)
        {
            distance = -distance;
        }
    }

    // Start is called before the first frame update
    void Update()
    {
        RaycastHit hit;
        RaycastHit hitOuter;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,30) && Physics.Raycast(transform.position + new Vector3(distance,0,0), transform.TransformDirection(Vector3.down), out hitOuter,30))
        {
            if (hit.collider.CompareTag("Road") && hitOuter.collider.CompareTag("Terrain"))
            {
                hittingEdge = true;
                onTerrain = false;
            }
            if (hit.collider.CompareTag("Road") && hitOuter.collider.CompareTag("Road"))
            {
                hittingEdge = false;
                onTerrain = false;
            }
            if (hit.collider.CompareTag("Terrain") && hitOuter.collider.CompareTag("Terrain"))
            {
                hittingEdge = false;
                onTerrain = true;
            }
        }
        Debug.DrawRay(transform.position,
            transform.TransformDirection(Vector3.down), Color.cyan);
        Debug.DrawRay(transform.position + new Vector3(distance,0f,0),
            transform.TransformDirection(Vector3.down), Color.magenta);

        
    }
}
