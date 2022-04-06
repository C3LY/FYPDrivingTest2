using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBridgeAnimation : MonoBehaviour
{
    [SerializeField] private GameObject animatedObject;
    private void OnTriggerEnter(Collider other)
    {
        print("play animation");
        animatedObject.SetActive(true);
            animatedObject.GetComponent<Animator>().SetTrigger("Run");
    }
}
