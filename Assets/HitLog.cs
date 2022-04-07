using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLog : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Vehicle")){
            GameManager.Instance.logToTextFileScenario(gameObject.name + " hit ");
        }
    }
}
