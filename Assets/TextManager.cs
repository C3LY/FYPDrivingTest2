using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class TextManager : MonoBehaviour
{
    private static TextManager _instance;
    public TextMeshProUGUI intro;
    [SerializeField] private TextMeshProUGUI GoogleFormText;
    [SerializeField] private TextMeshProUGUI GoogleFormID;
    [SerializeField] private TextMeshProUGUI UserGeneratedID;
    public TextMeshProUGUI endScreen;
    public static TextManager Instance
        {
            get
            {
                if(_instance ==null)
                    Debug.LogWarning("null text manager");

                return _instance;
            }
        }

    public void formShow(string Scenario)
    {
        GoogleFormText.gameObject.SetActive(true);
        GoogleFormID.text = Scenario;
        GoogleFormID.gameObject.SetActive(true);
    }

    public void formClose()
    {
        GoogleFormText.gameObject.SetActive(false);
        GoogleFormID.gameObject.SetActive(false);
    }
    
}
