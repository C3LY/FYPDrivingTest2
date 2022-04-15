using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    [SerializeField] private GameObject Instructions;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Instructions.SetActive(true);
        }
        else
        {
            Instructions.SetActive(false);
        }
    }
}
