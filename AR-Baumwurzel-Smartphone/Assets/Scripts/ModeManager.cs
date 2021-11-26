using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SwipeDetector;

public class ModeManager : MonoBehaviour
{
    public Slider sliderInstance;
    public Text modeDescription;
    public GameObject stencilWindowInstance;
    public GameObject arpointcloud;
    public GameObject rootsInstance1;
    public GameObject rootsInstance2;
    public GameObject ShaderLayerGroup;

    private bool rootSelect = true;
    private GameObject rootCurrent;
    private bool undergroundArea = false;
    private bool phantomLog = false;
    private bool shadowPlane = false;
    private bool useToggleButtons = false;

    private string[][] modeArray =
    {
        // Model 0
        new string[] { "window", "model" },

        // Stencil Variants 1-4
        new string[] { "model", "hole" },
        new string[] { "model", "hole", "underground" },
        new string[] { "model", "hole", "phantom" },
        new string[] { "model", "hole", "underground", "phantom" },

        // Dirt Variants 5-8
        new string[] { "model", "hole", "dirt" },
        new string[] { "model", "hole", "dirt", "underground"},
        new string[] { "model", "hole", "dirt", "phantom" },
        new string[] { "model", "hole", "dirt", "underground", "phantom" },

        // Leaves Variants 9-12
        new string[] { "model", "hole", "leaves" },
        new string[] { "model", "hole", "leaves", "underground"},
        new string[] { "model", "hole", "leaves", "phantom" },
        new string[] { "model", "hole", "leaves", "underground", "phantom" },

        // Semi-Transparent Variants 13-16
        new string[] { "model", "hole", "transparent_model" },
        new string[] { "model", "hole", "transparent_model", "underground" },
        new string[] { "model", "hole", "transparent_model", "phantom" },
        new string[] { "model", "hole", "transparent_model", "underground", "phantom" },

        // Gradient Layer Variants 17-20
        new string[] { "model", "bighole", "leaves_opacity_inc" },
        new string[] { "model", "bighole", "leaves_opacity_dec" },
        new string[] { "model", "bighole", "leaves_opacity_inc", "underground", "phantom" },
        new string[] { "model", "bighole", "leaves_opacity_dec", "underground", "phantom" },

        // Grid Variants 21-24
        new string[] { "model", "hole", "grid_color" },
        new string[] { "model", "hole", "grid_transparent" },
        new string[] { "model", "hole", "grid_color", "underground", "phantom" },
        new string[] { "model", "hole", "grid_transparent", "underground", "phantom" },

        // Pointcloud 25
        new string[] { "model", "hole", "pointcloud" }
    };


    private int[] modeSelection = new int[] {0,1,2,3,4,8,19,23,24};

    /* Currently not in use
    private string[] descriptionArray = new string[]
    {
        "V0: Model",
        "V1: Model, stencil buffer",
        "V1b: Model, stencil buffer, background occlusion, phantomobject",
        "V2: Model, stencil buffer, ground occludes edge",
        "V2b: Model, stencil buffer, ground occludes edge, background occlusion, phantomobject",
        "V3: Model, stencil buffer, leaves occlude edge",
        "V3b: Model, stencil buffer, leaves occlude edge, background occlusion, phantomobject",
        "V4a: Model, stencil buffer, semi-transparent model",
        "V4b: Model, stencil buffer, object occlude model/reality",
        "V4c: Model, stencil buffer, object occlude model/reality (reversed)",
        "V4d: Model, stencil buffer, object occlude model/reality, background occlusion, phantomobject",
        "V4e: Model, stencil buffer, object occlude model/reality(reversed), background occlusion, phantomobject",
        "V5a: Model, stencil buffer, grid layer (colored)",
        "V5b: Model, stencil buffer, grid layer (phantom)",
        "V6a: Model, stencil buffer, phantom features points"
    };
    */

    private void Awake()
    {
        OnSwipe += SwipeDetector_OnSwipe;
    }

    private void Start()
    {
        int minVal = 0;

        sliderInstance.minValue = minVal;
        sliderInstance.maxValue = modeSelection.Length - 1;
        sliderInstance.wholeNumbers = true;

        rootCurrent = rootsInstance1;
        SetModeDescription(minVal);

        ResetObjects();
        stencilWindowInstance.SetActive(true);
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if(data.Direction.ToString().Equals("Left"))
        {
            if(sliderInstance.value < sliderInstance.maxValue)
            {
                sliderInstance.value++;
            }
        }
        else if (data.Direction.ToString().Equals("Right"))
        {
            if (sliderInstance.value > sliderInstance.minValue)
            {
                sliderInstance.value--;
            }
        }
    }
    
    public void SwitchRoots()
    {
        rootSelect = !rootSelect;

        if(rootSelect)
        {
            rootCurrent = rootsInstance1;
        }
        else
        {
            rootCurrent = rootsInstance2;
        }

        OnValueChanged(sliderInstance.value);
    }

    public void ToggleUnderground()
    {
        ChangeButton(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>());
        undergroundArea = !undergroundArea;
        OnValueChanged(sliderInstance.value);
    }

    public void ToggleLog()
    {
        ChangeButton(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>());
        phantomLog = !phantomLog;
        OnValueChanged(sliderInstance.value);
    }

    public void ToggleShadow()
    {
        ChangeButton(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>());
        shadowPlane = !shadowPlane;
        OnValueChanged(sliderInstance.value);
    }

    private void ChangeButton(Text textComp)
    {
        if (textComp.text.Contains("Off"))
        {
            textComp.text = textComp.text.Replace("Off", "On");
            textComp.color = new Color(0, 255, 0, 255);
        }
        else
        {
            textComp.text = textComp.text.Replace("On", "Off");
            textComp.color = new Color(255, 0, 0, 255);
        }
    }

    public void OnValueChanged(float value)
    {
        ResetObjects();
        int val = modeSelection[(int)value];

        if (!useToggleButtons)
        {
            undergroundArea = modeArray[val].Contains("underground");
            phantomLog = modeArray[val].Contains("phantom");
            shadowPlane = modeArray[val].Contains("shadow");
        }

        // Shader Window
        stencilWindowInstance.SetActive(modeArray[val].Contains("window"));

        // Root Model Group
        rootCurrent.SetActive(modeArray[val].Contains("model"));

        rootCurrent.transform.Find("Leaves").gameObject.SetActive(modeArray[val].Contains("leaves"));

        rootCurrent.transform.Find("Dirt").gameObject.SetActive(modeArray[val].Contains("dirt"));

        rootCurrent.transform.Find("SemiTransparentRoots").gameObject.SetActive(modeArray[val].Contains("transparent_model"));

        rootCurrent.transform.Find("LeaveLayerT2O").gameObject.SetActive(modeArray[val].Contains("leaves_opacity_inc"));

        rootCurrent.transform.Find("LeaveLayerO2T").gameObject.SetActive(modeArray[val].Contains("leaves_opacity_dec"));

        // Shader Layer Group
        ShaderLayerGroup.transform.Find("Hole").gameObject.SetActive(modeArray[val].Contains("hole"));

        ShaderLayerGroup.transform.Find("Hole_Big").gameObject.SetActive(modeArray[val].Contains("bighole"));

        ShaderLayerGroup.transform.Find("GridColored").gameObject.SetActive(modeArray[val].Contains("grid_color"));

        ShaderLayerGroup.transform.Find("GridTransparent").gameObject.SetActive(modeArray[val].Contains("grid_transparent"));

        ShaderLayerGroup.transform.Find("UndergroundArea").gameObject.SetActive(undergroundArea);

        ShaderLayerGroup.transform.Find("PhantomLog").gameObject.SetActive(phantomLog);

        ShaderLayerGroup.transform.Find("ShadowPlane").gameObject.SetActive(shadowPlane);

        // AR Pointcloud
        arpointcloud.SetActive(modeArray[val].Contains("pointcloud"));

        // Update mode description
        SetModeDescription((int)value);
    }

    private void SetModeDescription(int value)
    {
        modeDescription.text = "Mode: " + (value+1);
        // modeDescription.text = descriptionArray[value];
    }

    public void ResetObjects()
    {
        // Set Window inactive
        stencilWindowInstance.SetActive(false);

        // Set PointCloud inactive
        arpointcloud.SetActive(false);

        // Set Root1 inactive with all children
        rootsInstance1.SetActive(false);
        for (int i = 0; i < rootsInstance1.transform.childCount; i++)
        {
            rootsInstance1.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Set Root2 inactive with all children
        rootsInstance2.SetActive(false);
        for (int i = 0; i < rootsInstance2.transform.childCount; i++)
        {
            rootsInstance2.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Set Shader Layer inactive
        for (int i = 0; i < ShaderLayerGroup.transform.childCount; i++)
        {
            ShaderLayerGroup.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
