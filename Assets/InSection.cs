using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            GameManager.Instance.sectionNumber = gameObject.name;
        }
    }
}
