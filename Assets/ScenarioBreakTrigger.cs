using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioBreakTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
    GameManager.Instance.logToTextFileScenario("Reached Scenario Breaker --");
        GameManager.Instance.switchToNextScenario();
    }
}
