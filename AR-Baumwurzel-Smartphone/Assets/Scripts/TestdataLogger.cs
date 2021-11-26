using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestdataLogger : MonoBehaviour
{

    private string filePath;

    //Create Textfile if it doesn't exist
    void CreateText()
    {
        filePath = Application.persistentDataPath + "/Log_ARSmartphone.txt";
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "AR Baumwurzel Smartphone Logging\n====================================\n");
        }
    }

    void AppendText(string text)
    {
        CreateText();
        if (File.Exists(filePath))
        {
            File.AppendAllText(filePath, "Logging: " + System.DateTime.Now + " [" + text + "]\n");
        }
    }

    void Start()
    {
        AppendText("Start App");
    }

    public void LogButtonClick()
    {
        string text = EventSystem.current.currentSelectedGameObject.transform.GetComponentInChildren<Text>().text;
        text = text.Replace("\n", "").Replace("\r", "");
        AppendText(text);
    }

    public void LogValueChanged(float value)
    {
        AppendText("Mode changed: " + value);
    }

    public void LogObjectPlacement()
    {
        AppendText("Object placed");
    }
}
