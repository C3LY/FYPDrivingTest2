using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEdgeWarning : MonoBehaviour
{

    [SerializeField] private RaycastDummy _dummyFL;
    [SerializeField] private RaycastDummy _dummyFR;
    [SerializeField] private RaycastDummy _dummyRL;
    [SerializeField] private RaycastDummy _dummyRR;

    private bool alreadyOnTerrain;
    private void FixedUpdate()
    {
//print(alreadyOnTerrain + "FL " + _dummyFL.onTerrain + " FR" + _dummyFR.onTerrain + " RL" + _dummyRL.onTerrain + " RR" + _dummyRR.onTerrain);
        if(!GameManager.Instance.sectionNumber.Contains("Start")){
            if (_dummyFL.hittingEdge || _dummyFR.hittingEdge || _dummyRL.hittingEdge || _dummyRR.hittingEdge)
            {
                GameManager.Instance.logToTextFileScenario("Close to edge > " + GameManager.Instance.sectionNumber);
                GameManager.Instance.edgeCounterPerScenario++;
            }

            if (_dummyFL.onTerrain || _dummyFR.onTerrain || _dummyRL.onTerrain || _dummyRR.onTerrain)
            {
                if (!alreadyOnTerrain)
                {
                    GameManager.Instance.logToTextFileScenario("On Terrain > " + GameManager.Instance.sectionNumber);
                    alreadyOnTerrain = true;
                    GameManager.Instance.terrainCounterPerScenario++;
                }
            }
            else
            {
                alreadyOnTerrain = false;
            }
        }
    }
}
