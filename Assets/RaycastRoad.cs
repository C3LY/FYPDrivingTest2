using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRoad : MonoBehaviour
{
    public int vectorX;
    public int vectorY ;
    public int vectorZ;
    
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

        double hitAdjacentLength = Math.Sqrt((hitHypotenuse.distance * hitHypotenuse.distance) - (0.5*0.5)); // find adjacent length using basic trig
        print("hit  adjacent distance " + hitAdjacentLength);
        print(hitDownOpposite.distance);
    }
    

}
