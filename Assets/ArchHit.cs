using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchHit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision: " + other.collider.name);
        GameManager.Instance.logToTextFileScenario(" - Arch Hit ");
    }
}
