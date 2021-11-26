using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles functionalities of the scene selection window which can be called via speech command: "Szenen".
/// </summary>
public class SceneSelect : MonoBehaviour
{
    [SerializeField]
    GameObject menu = null;

    [SerializeField]
    TextMeshPro timeDisplay = null;

    [SerializeField]
    bool loadSceneOnStart = false;

    [SerializeField]
    string startupSceneName;

    List<float> times = new List<float>();

    string defaultTimeString = null;

    public int sceneIteration { get; private set; } = 1;

/// <summary>
/// Loads desired scene on app startup if enabled.
/// </summary>
    void Start()
    {
        defaultTimeString = timeDisplay.text;

        if (loadSceneOnStart)
        {
            LoadScene(startupSceneName);
        }
    }

/// <summary>
/// Positions scene selection windows in front of the player. Always called when scene selection window opens.
/// </summary>
    void PositionInFrontOfPlayer()
    {
        Vector3 destPos = Camera.main.transform.position;
        Vector3 destRot = transform.eulerAngles;

        destPos += Camera.main.transform.forward * 0.9f;
        destRot.y = Camera.main.transform.eulerAngles.y;

        transform.position = destPos;
        transform.eulerAngles = destRot;
    }

/// <summary>
/// Shows/Hides scene selection window.
/// </summary>
    public void SetMenuActive(bool state)
    {
        menu.SetActive(state);

        if (state) PositionInFrontOfPlayer();
    }

/// <summary>
/// Loads a new scene.
/// </summary>
    public void LoadScene(string sceneName)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();

        sceneSystem.LoadContent(sceneName, LoadSceneMode.Single);

        SetMenuActive(false);
    }
/// <summary>
/// Runs necessary coroutine to reaload a scene.
/// </summary>
    public void ReloadScene(string sceneName)
    {
        StartCoroutine(ReloadSceneCoroutine(sceneName));
    }

/// <summary>
/// Realods a scene by speech command. Keeps transformation of the root, but resets all further progressions.
/// </summary>
    IEnumerator ReloadSceneCoroutine(string sceneName)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();

        GameObject positionerObject = GameObject.FindGameObjectWithTag("PositionerObject");

        Vector3 pos = positionerObject.transform.position;
        Vector3 rot = positionerObject.transform.eulerAngles;
        Vector3 scale = positionerObject.transform.localScale;

        sceneSystem.UnloadContent(sceneName);

        yield return new WaitForSeconds(0.5f);

        sceneSystem.LoadContent(sceneName, LoadSceneMode.Single);

        yield return new WaitForSeconds(0.5f);

        positionerObject = GameObject.FindGameObjectWithTag("PositionerObject");

        positionerObject.transform.position = pos;
        positionerObject.transform.eulerAngles = rot;
        positionerObject.transform.localScale = scale;

        positionerObject.GetComponent<NearInteractionGrabbable>().enabled = false;
        positionerObject.GetComponent<MeshRenderer>().enabled = false;
        positionerObject.GetComponent<ObjectManipulator>().enabled = false;
        positionerObject.transform.GetChild(2).gameObject.SetActive(false);
        positionerObject.transform.GetChild(3).gameObject.SetActive(true);
    }

/// <summary>
/// Saves final time of collecting leaves challenge in a list.
/// </summary>
    public void SaveTime(float time)
    {
        times.Add(time);

        UpdateTimeDisplay();
    }

/// <summary>
/// Increases scene iteration by 1 (new iterations swap leaf positions in the collecting leaves challenge).
/// </summary>
    public void NextSceneIteration()
    {
        sceneIteration++;
    }

/// <summary>
/// Displays final times of every iteration in the scene selection menu.
/// </summary>
    private void UpdateTimeDisplay()
    {
        if (times.Count <= 0)
            timeDisplay.text = defaultTimeString;
        else
        {
            timeDisplay.text = "";

            for (int i = 0; i < times.Count; i++)
            {
                timeDisplay.text += "Time " + i + 1 + ": " + System.Math.Round(times[i], 3).ToString() + "\n";
            }
        }
    }
}
