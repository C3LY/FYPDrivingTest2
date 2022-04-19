using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    [SerializeField] private GameObject Instructions;
    [SerializeField] private GameObject TextFillGoogle; // checks if the scenario is not running yet
    private bool IPressed = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I) && !(TextFillGoogle.activeInHierarchy))
        {
            IPressed = true;
            Time.timeScale = 0;
            Instructions.SetActive(true);
        }
        else
        {
            if(!TextFillGoogle.activeInHierarchy){
                Instructions.SetActive(false);
                if (IPressed)
                {
                    Time.timeScale = 1;
                }

                IPressed = false;
            }
        }
    }
}
