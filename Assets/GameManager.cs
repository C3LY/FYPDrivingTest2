using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
    public string filename;
    public string fileText;
    [SerializeField] private GameObject startingLocationBridge;
    [SerializeField] private GameObject startingLocationMountain;
    [SerializeField] private GameObject playerAmbulance;

    [SerializeField] private TextMeshProUGUI ScenarioText;
    [SerializeField] private GameObject FillFormText;
    [SerializeField] private TextMeshProUGUI Code;
    [SerializeField] private GameObject FillFormCloseButton;
    [SerializeField] private GameObject EndScreen;
    
    List<Tuple<GameObject, string>> shuffledScenarioList = new List<Tuple<GameObject, string>>();
    List<Tuple<GameObject, string>> scenarioList = new List<Tuple<GameObject, string>>();
    private List<string> scenarioName = new List<string>();

    public int edgeCounterPerScenario;
    public int terrainCounterPerScenario;
    public string sectionNumber;
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
        ScenarioText.SetText(shuffledScenarioList[currentScenario].Item2 + " - " + shuffledScenarioList[currentScenario].Item1.name);
        foreach (Tuple<GameObject, string> obj in scenarioList)
        {
            obj.Item1.SetActive(false);
        }
        
        shuffledScenarioList[currentScenario].Item1.SetActive(true);
        edgeCounterPerScenario = 0;
        terrainCounterPerScenario = 0;

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
    
    private void setUpSpecificScenario(int scenarioNumber)
    {
        logToTextFileScenario("************ Specific scenario:  " + scenarioNumber);
        ScenarioText.SetText(scenarioList[scenarioNumber].Item2 + " - " + scenarioList[scenarioNumber].Item1.name);
        foreach (Tuple<GameObject, string> obj in scenarioList)
        {
            obj.Item1.SetActive(false);
        }

        if(scenarioNumber<scenarioList.Count){
            scenarioList[scenarioNumber].Item1.SetActive(true);

            if (scenarioList[scenarioNumber].Item2 == scenarioName[0])
            {
                playerAmbulance.transform.rotation = startingLocationMountain.transform.rotation;
                playerAmbulance.transform.position = startingLocationMountain.transform.position;
                playerAmbulance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            else
            {
                playerAmbulance.transform.rotation = startingLocationBridge.transform.rotation;
                playerAmbulance.transform.position =
                    startingLocationBridge.transform
                        .position; //TODO: if time, make generic instead of serializable fields
                playerAmbulance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }

    private void TextFill()
    {
        Time.timeScale = 0;
        if (currentScenario<shuffledScenarioList.Count)
        {
            String scenario = shuffledScenarioList[currentScenario].Item2 + " - "  + shuffledScenarioList[currentScenario].Item1.name;
            if (scenario.Contains("Control"))
            {
                Code.text = "Control scenario, no need to fill in form";
            }
            else
            {
                Code.text = scenario;
            }
            FillFormText.SetActive(true);
            Code.gameObject.SetActive(true);
            FillFormCloseButton.SetActive(true);
        }
    }

    public void switchToNextScenario()
    {
        TextFill();
        logToTextFileScenario("Counter Edge: " + edgeCounterPerScenario);
        logToTextFile("Counter Terrain: " + terrainCounterPerScenario);
        currentScenario++;
        if (currentScenario >= shuffledScenarioList.Count)
        {
            print("Finished scenarios.");
            foreach (Tuple<GameObject, string> obj in scenarioList)
            {
                obj.Item1.SetActive(false);
            }
            EndScreen.gameObject.SetActive(true);
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
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            logToTextFileScenario("*****RESET****");
            setUpScenario();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            setUpSpecificScenario(0);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setUpSpecificScenario(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setUpSpecificScenario(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setUpSpecificScenario(3);
        }
        //---------
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            setUpSpecificScenario(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            setUpSpecificScenario(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            setUpSpecificScenario(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            setUpSpecificScenario(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            setUpSpecificScenario(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            setUpSpecificScenario(9);
        }
    }

    private void logToTextFile(string lineOfText)
    {
        try {
            System.IO.File.AppendAllText(filename, DateTime.Now + lineOfText + "\n"); //For downloaded projects
            fileText += (DateTime.Now + lineOfText + "\n");
        }
        catch {
            print("could not append");
        }
    }
    
    public void logToTextFileScenario(string lineOfText)
    {
        if(currentScenario<shuffledScenarioList.Count){
            logToTextFile(
                "  " + shuffledScenarioList[currentScenario].Item2 + " | " +
                shuffledScenarioList[currentScenario].Item1.name + " | " + lineOfText);
        }

    }
    
}
