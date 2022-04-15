using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseToEdgeWarning : MonoBehaviour
{
    [SerializeField] private RaycastRoad FL;
    [SerializeField] private RaycastRoad FR;
    [SerializeField] private RaycastRoad RL;
    [SerializeField] private RaycastRoad RR;
    
    [SerializeField] private RaycastDummy _dummyFL;
    [SerializeField] private RaycastDummy _dummyFR;
    [SerializeField] private RaycastDummy _dummyRL;
    [SerializeField] private RaycastDummy _dummyRR;

    [SerializeField] private GameObject warningUI;
//    [SerializeField] private float distanceWarning = 0.01f;
    private void FixedUpdate()
    {
//        if (FL.distanceFromEdge <= distanceWarning||FR.distanceFromEdge <= distanceWarning||RL.distanceFromEdge <= distanceWarning||RR.distanceFromEdge <= distanceWarning)
//        {
//            warningUI.SetActive(true);
//        }
//        else
//        {
//            warningUI.SetActive(false);
//        }

        if (_dummyFL.hittingEdge || _dummyFR.hittingEdge || _dummyRL.hittingEdge || _dummyRR.hittingEdge)
        {
            warningUI.SetActive(true);
            GameManager.Instance.logToTextFileScenario("Close to edge");
        }
        else
        {
            warningUI.SetActive(false);
        }
        
        if (_dummyFL.onTerrain || _dummyFR.onTerrain || _dummyRL.onTerrain || _dummyRR.onTerrain)
        {
            GameManager.Instance.logToTextFileScenario("On Terrain");
        }
    }
}
