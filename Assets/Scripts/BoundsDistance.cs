using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoundsDistance : MonoBehaviour
{
    public float hitPoints = 100.0F;
    public Collider coll;
    void Start()
    {
        coll = GetComponent<Collider>();
    }

//    void ApplyHitPoints(Vector3 explosionPos, float radius)
//    {
//        // The distance from the explosion position to the surface of the collider.
//        Vector3 closestPoint = coll.ClosestPointOnBounds(explosionPos);
//        float distance = Vector3.Distance(closestPoint, explosionPos);
//
//        // The damage should decrease with distance from the explosion.
//        float damage = 1.0F - Mathf.Clamp01(distance / radius);
//        hitPoints -= damage * 10.0F;
//    }

    private void Update()
    {
        print(coll.ClosestPoint(transform.position));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(coll.ClosestPoint(transform.position),new Vector3(.2f,.2f,.2f));
    }
}
