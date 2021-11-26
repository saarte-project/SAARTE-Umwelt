using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControls : MonoBehaviour
{
    private GameObject targetObject;

    private bool isActive = true;

    public void toggleDirt()
    {
        toggleLayer("Dirt");
    }

    public void toggleLeaves()
    {
        toggleLayer("Leaves");
    }

    private void toggleLayer(string layerName)
    {
        Text buttonTxt = transform.Find("Text").GetComponent<Text>();
        string state;

        targetObject = GameObject.FindGameObjectWithTag("Placed Roots").GetComponent<ARTapToPlaceObject>().objectClone.transform.Find(layerName).gameObject;

        isActive = !isActive;

        if (isActive)
        {
            targetObject.SetActive(true);
            state = "on";
        }
        else
        {
            targetObject.SetActive(false);
            state = "off";
        }

        buttonTxt.text = layerName + ": " + state;
    }
}
