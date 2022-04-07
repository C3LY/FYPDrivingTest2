using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class RaycastRoad : MonoBehaviour
{
    [SerializeField] private int vectorX;
    [SerializeField] private int vectorY = -1;
    [SerializeField] private int vectorZ =  10;
    [SerializeField] private float threshold = 0.01f;
    [SerializeField] private int sweepDistance = 10;
    [SerializeField] private bool showGizmos = false;

    [SerializeField] private TextMeshProUGUI uiElement;
    private Vector3 gizmoHitPoint = new Vector3();

    public float distanceFromEdge = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitDownOpposite;
        RaycastHit hitHypotenuse;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDownOpposite, sweepDistance))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(vectorX, vectorY, vectorZ),
                out hitHypotenuse, sweepDistance))
            {
                if (showGizmos)
                {
                    Debug.DrawRay(transform.position,
                        transform.TransformDirection(vectorX, vectorY, vectorZ) * hitHypotenuse.distance, Color.yellow);
                }

                if (hitDownOpposite.collider.CompareTag("Terrain") && hitHypotenuse.collider.CompareTag("Terrain"))
                {
                    uiElement.text = "on terrain ";
//                    print("on terrain");
//                    GameManager.Instance.logToTextFileScenario("On Terrain");
                }
                else
                {
                    float distanceZ = binarySearch();
                    distanceFromEdge = Mathf.Sqrt((distanceZ*distanceZ)-(hitDownOpposite.distance*hitDownOpposite.distance));
                    if (distanceZ <= 0f)
                    {
                        print(gameObject.name + " I'm on the edge");
                    }

                    uiElement.text = distanceZ + " ";
                }
            }

        }
    
    }


    private float binarySearch()
    {
        int maxLoop = 50;
        int loopCounter = 0;
        float tempVectorZ = (float)vectorZ;
        float leftPointer = 0;
        float middle = Mathf.Round((leftPointer + (tempVectorZ - leftPointer) / 2) * 100.0f) * 0.01f;
        while (loopCounter< maxLoop && Mathf.Abs(leftPointer - tempVectorZ) >= threshold)
        {
            loopCounter++;
            RaycastHit hit;
            middle = (leftPointer + (tempVectorZ - leftPointer) / 2);

            if(showGizmos){
                Debug.Log(leftPointer + " " + middle + " " + tempVectorZ);
            }
            if (Physics.Raycast(transform.position, transform.TransformDirection(0, -1, middle), out hit, sweepDistance))
            {
                gizmoHitPoint = hit.point;
                if (isEdge(middle))
                {
                    return middle;
                }
                if (hit.collider.transform.CompareTag("Road"))
                {
                    leftPointer = (float)System.Math.Round(middle,4);
                }
            
                else if (hit.collider.transform.CompareTag("Terrain"))
                {
//                    tempVectorZ = (float)(middle);
                    tempVectorZ = (float)System.Math.Round(middle,4);
                }
                else
                {
                    print("bail - no tag");
                    return Mathf.Infinity;
                }
            }

            else
            {
                print("bail - no raycast");
                return Mathf.Infinity;
                
            }
            
        }

        if (loopCounter>=maxLoop)
        {
            Debug.LogWarning("WARNING - loop breached");
        }

        if(showGizmos)
        {
            Debug.DrawRay(transform.position, transform.transform.TransformDirection(0, -1, middle) * sweepDistance, Color.green);
            Debug.Log("end: " + leftPointer + " " + middle + " " + tempVectorZ);
            Debug.Log("is this the edge? " + isEdge(middle) + "---------------------------");
        }
        return middle;
    }

    //testing function
    private bool isEdge(float vectorZToLookAt)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(0,-1,vectorZToLookAt), out hit, sweepDistance);
        RaycastHit hitPlus;
        Physics.Raycast(transform.position, transform.TransformDirection(0,-1,(float)(vectorZToLookAt +0.2)), out hitPlus, sweepDistance);
        if (hit.collider.transform.CompareTag("Road"))
        {
            if(hitPlus.collider.transform.CompareTag("Terrain")){
                print("this is  the  edge");
                return true;
            }
        }
        //   print("the point looked at " + vectorZToLookAt + "is not the edge" + "hitCollider" + hit.collider.name);
            return false;
    }

    private void OnDrawGizmos()
    {

        if(showGizmos){
            Gizmos.DrawSphere(gizmoHitPoint, 0.5f);
        }
    }
}
