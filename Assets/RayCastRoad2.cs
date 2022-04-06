using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastRoad2 : MonoBehaviour
{
    public int vectorX;
    public int vectorY ;
    public int vectorZ = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitHypotenuse;
        Debug.DrawRay(transform.position, transform.TransformDirection(vectorX, vectorY, vectorZ) * 5, Color.red);

        if (        Physics.Raycast(transform.position, transform.TransformDirection(vectorX, vectorY, vectorZ), out hitHypotenuse,
            10))
        {
            print(hitHypotenuse.collider.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(vectorX, vectorY, vectorZ) * hitHypotenuse.distance, Color.green);
//            Debug.Log("Collider: " + transform.name + "Collided with: " + hitHypotenuse.collider.name + "hit distance: " + hitHypotenuse.distance);
            if (hitHypotenuse.collider.CompareTag("Road"))
            {
                print("Raycast road distance " + hitHypotenuse.distance);
            }
        }
        
        
    }
}
