using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerActive : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        print("trigger enter");
        if (other.gameObject.CompareTag("Player"))
        {
            print("Vehicle trigger");
            obj.SetActive(true);
        }
    }
}
