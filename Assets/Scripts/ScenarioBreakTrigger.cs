using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioBreakTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Vehicle"))
        {
            GameManager.Instance.logToTextFileScenario("Reached Scenario Breaker --");
            GameManager.Instance.switchToNextScenario();
        }
    }
}
