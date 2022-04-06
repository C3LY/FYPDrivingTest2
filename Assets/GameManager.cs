using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startingLocationBridge;
    [SerializeField] private GameObject startingLocationMountain;
    [SerializeField] private GameObject playerAmbulance;
    
    List<Tuple<GameObject, string>> shuffledScenarioList = new List<Tuple<GameObject, string>>();
    List<Tuple<GameObject, string>> scenarioList = new List<Tuple<GameObject, string>>();
    private List<string> scenarioName = new List<string>();
    public int currentScenario { get; set; }

    private static Random rng = new Random();
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance ==null)
                Debug.LogWarning("null game manager");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        foreach (var obj in GameObject.FindGameObjectsWithTag("ScenarioList"))
        {
            scenarioName.Add(obj.name);
//            print("found objects in scenarioList" + obj.name);
            foreach (Transform child in obj.transform)
            {
                scenarioList.Add(new Tuple<GameObject,string>(child.gameObject,obj.name));
            }
        }
        string fullList = "shuffled list : " ;
        shuffledScenarioList = scenarioList.OrderBy(a => rng.Next()).ToList();
        foreach( var x in shuffledScenarioList) {
            fullList += x.ToString() + " ";
        }
        print(fullList);
    }
    
    private void Start()
    {
        setUpScenario();

    }

    private void setUpScenario()
    {
        print("current scenario: " + currentScenario + " -> " + shuffledScenarioList[currentScenario].Item1.name);
        foreach (Tuple<GameObject, string> obj in scenarioList)
        {
            obj.Item1.SetActive(false);
        }
        
        shuffledScenarioList[currentScenario].Item1.SetActive(true);

        if (shuffledScenarioList[currentScenario].Item2 == scenarioName[0])
        {
            playerAmbulance.transform.rotation = startingLocationMountain.transform.rotation;
            playerAmbulance.transform.position = startingLocationMountain.transform.position;
        }
        else
        {
            playerAmbulance.transform.rotation = startingLocationBridge.transform.rotation;
            playerAmbulance.transform.position = startingLocationBridge.transform.position; //TODO: if time, make generic instead of serializable fields
        } 
    }

    public void switchToNextScenario()
    {

        currentScenario++;
        if (currentScenario >= shuffledScenarioList.Count)
        {
            print("Finished scenarios.");
            Application.Quit();
        }
        else
        {
            setUpScenario(); 
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            switchToNextScenario();
        }
    }
}
