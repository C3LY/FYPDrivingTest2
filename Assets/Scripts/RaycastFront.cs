using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFront : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,30);
        Debug.DrawRay(transform.position,
            transform.TransformDirection(Vector3.forward), Color.blue);
    }
}
