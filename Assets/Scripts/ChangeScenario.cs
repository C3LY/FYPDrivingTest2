//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class ChangeScenario : MonoBehaviour
//{
//    [SerializeField] private GameObject startingLocationBridge;
//    [SerializeField] private GameObject startingLocationMountain;
//    [SerializeField] private GameObject playerAmbulance;
//    [SerializeField] private GameObject BvirtualAmbulance;
//    [SerializeField] private GameObject BdimensionSquare;
//    [SerializeField] private GameObject Bsignage;
//    [SerializeField] private GameObject BcanvasWarning;
//
//    [SerializeField] private GameObject MvirtualAmbulance;
//    [SerializeField] private GameObject Mbouldards;
//    [SerializeField] private GameObject MedgeWarning;
//    [SerializeField] private GameObject MedgeStats;
//    [SerializeField] private GameObject MvirtualCam;
//
//    /*
//     * get keydown
//     * set active
//     * set coordinate
//     *
//     * TODOs:
//     * Collider barrier edge of road
//     * Boulards
//     *  control/animated of van
//     * Animated virtual van
//     * delete if van hits edge of bridge
//     */
//    private void HideEverything()
//    {
//        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Scenario"))
//        {
//            obj.SetActive(false);
//        }
//    }
//
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Alpha0))
//        {
//            HideEverything();
//            print("Signage Scenario");
//            playerAmbulance.transform.position = startingLocationBridge.transform.position;
//            Bsignage.SetActive(true);
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            HideEverything();
//            print("Dimension Square Scenario");
//            playerAmbulance.transform.position = startingLocationBridge.transform.position;
//            BdimensionSquare.SetActive(true);
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            HideEverything();
//            print("Animation Ambulance Scenario");
//            playerAmbulance.transform.position = startingLocationBridge.transform.position;
//            BvirtualAmbulance.SetActive(true);
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha3))
//        {
//            HideEverything();
//            print("Canvas Warning Scenario");
//            playerAmbulance.transform.position = startingLocationBridge.transform.position;
//            BcanvasWarning.SetActive(true);
//        }
//        //---------
//        else if (Input.GetKeyDown(KeyCode.Alpha6))
//        {
//            HideEverything();
//            print("Virtual Boulard Scenario");
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha7))
//        {
//            HideEverything();
//            print("Canvas Warning Message Scenario");
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha8))
//        {
//            HideEverything();
//            print("Canvas Warning Stats Scenario");
//        }
//        else if (Input.GetKeyDown(KeyCode.Alpha9))
//        {
//            HideEverything();
//            print("Animation Ambulance Scenario");
//        }
//
//        // doorway visualisation?
//    }
//}