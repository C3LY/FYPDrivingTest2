using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
    private string filename;
    [SerializeField] private GameObject startingLocationBridge;
    [SerializeField] private GameObject startingLocationMountain;
    [SerializeField] private GameObject playerAmbulance;
    
    [SerializeField] private GameObject FillFormText;
    [SerializeField] private TextMeshProUGUI Code;
    [SerializeField] private GameObject FillFormCloseButton;
    
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
    // once Unity analytics is working
//    async void Start()
//    {
//        await UnityServices.InitializeAsync();
//        Events.CheckForRequiredConsents();
//
//    }

    private void setUpLogFile()
    {
        string d = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        d = d + "/DrivingSimulationLOGS";
 
        System.IO.Directory.CreateDirectory(d);
// that command means "create if not already there, otherwise leave it alone"

        filename= d + "/log" + DateTime.Now.Day + "D" + DateTime.Now.Month + "M" + DateTime.Now.Year + "Y" + DateTime.Now.Hour + "H" + DateTime.Now.Minute + "M" + ".txt";
        print(filename);
    }
    private void Awake()
    {
        _instance = this;
        setUpLogFile();
        logToTextFile("----" + "Start" + "----");
        foreach (var obj in GameObject.FindGameObjectsWithTag("ScenarioList"))
        {
            scenarioName.Add(obj.name);
//            print("found objects in scenarioList" + obj.name);
            foreach (Transform child in obj.transform)
            {
                scenarioList.Add(new Tuple<GameObject,string>(child.gameObject,obj.name));
            }
        }
        string fullList = " Shuffled list order : " ;
        shuffledScenarioList = scenarioList.OrderBy(a => rng.Next()).ToList();
        foreach( var x in shuffledScenarioList) {
            fullList += x.ToString() + " ";
        }
        print(fullList);
        logToTextFile(fullList);
    }
    
    private void Start()
    {
        Time.timeScale = 0;
        setUpScenario();

    }

    private void setUpScenario()
    {
        logToTextFileScenario(" Next scenario:  " + currentScenario);
        foreach (Tuple<GameObject, string> obj in scenarioList)
        {
            obj.Item1.SetActive(false);
        }
        
        shuffledScenarioList[currentScenario].Item1.SetActive(true);

        if (shuffledScenarioList[currentScenario].Item2 == scenarioName[0])
        {
            playerAmbulance.transform.rotation = startingLocationMountain.transform.rotation;
            playerAmbulance.transform.position = startingLocationMountain.transform.position;
            playerAmbulance.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
        else
        {
            playerAmbulance.transform.rotation = startingLocationBridge.transform.rotation;
            playerAmbulance.transform.position = startingLocationBridge.transform.position; //TODO: if time, make generic instead of serializable fields
            playerAmbulance.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        } 
    }

    private void TextFill()
    {
        Time.timeScale = 0;
        Code.text = shuffledScenarioList[currentScenario].Item2 + shuffledScenarioList[currentScenario].Item1.name;
        FillFormText.SetActive(true);
        Code.gameObject.SetActive(true);
        FillFormCloseButton.SetActive(true);
    }

    public void switchToNextScenario()
    {
        TextFill();
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

    public void resumeTime()
    {
        Time.timeScale = 1;
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

    public void logToTextFile(string lineOfText)
    {
        try {
            System.IO.File.AppendAllText(filename, DateTime.Now + lineOfText + "\n");
        }
        catch {
            print("could not append");
        }
    }
    
    public void logToTextFileScenario(string lineOfText)
    {
        logToTextFile(
                    "  " + shuffledScenarioList[currentScenario].Item2 + " | " +
                    shuffledScenarioList[currentScenario].Item1.name + " | " + lineOfText);

    }
    
}
