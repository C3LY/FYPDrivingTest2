using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRoad : MonoBehaviour
{
    public int vectorX;
    public int vectorY ;
    public int vectorZ;

    public Collider roadCollider;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitDownOpposite;
        RaycastHit hitHypotenuse;
        
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDownOpposite, 10);
        if (Physics.Raycast(transform.position, transform.TransformDirection(vectorX, vectorY, vectorZ), out hitHypotenuse, 10))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(vectorX, vectorY, vectorZ) * hitHypotenuse.distance, Color.yellow);
//            Debug.Log("Collider: " + transform.name + "Collided with: " + hitHypotenuse.collider.name + "hit distance: " + hitHypotenuse.distance);
        }

        // as it is coming from a vector, it this all redundant?
        double hitAdjacentLength = Math.Sqrt((hitHypotenuse.distance * hitHypotenuse.distance) - (0.5*0.5)); // find adjacent length using basic trig
        print("hit  adjacent distance " + hitAdjacentLength);
        print(hitDownOpposite.distance);

        isEdge(vectorZ);
    }


    private float binarySearch()
    {
        float tempVectorZ = (float)vectorZ;
        float leftPointer = 0;
        while (leftPointer <= tempVectorZ)
        {
            float middle = (float)(leftPointer + (tempVectorZ - leftPointer) / 2);
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(0, -1, middle), out hit, 10);
            
            if (isEdge(middle))
            {
                return middle;
            }

            if (hit.collider.name == roadCollider.name)
            {
                leftPointer = (float)(middle + 0.2);
            }
            
            if (hit.collider.name == "terrain")
            {
                leftPointer = (float)(middle - 0.2);
            }
        }

        return 0f; // change to correct
    }

    private bool isEdge(float vectorZToLookAt)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(0,-1,vectorZToLookAt), out hit, 10);
        RaycastHit hitPlus;
        Physics.Raycast(transform.position, transform.TransformDirection(0,-1,(float)(vectorZToLookAt +0.2)), out hitPlus, 10);
        if (hit.collider.name == roadCollider.name && hitPlus.collider.name == "terrain")
        {
            print("this is  the  edge");
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(hit.point, (float)0.2);
            return true;
        }
        else
        {
            print("the point looked at " + vectorZToLookAt + "is not the edge");
            return false;
        }
    }
}
