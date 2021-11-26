using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit.Input;

public class SetBasePosition : MonoBehaviour, IMixedRealityInputHandler
{

    private GameObject basePoint;

    private bool locked;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Casting script.");
        locked = false;
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        bool successful = Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hitInfo,
                20.0f,
                Physics.DefaultRaycastLayers);
        if (successful &  Input.anyKey)
        {
            // If the Raycast has succeeded and hit a hologram
            // hitInfo's point represents the position being gazed at
            // hitInfo's collider GameObject represents the hologram being gazed at

            Debug.Log("Hit at (for base position):"+hitInfo.point);
            
            basePoint = GameObject.Find("BasePosition");
            basePoint.transform.localPosition = hitInfo.point;
            locked = false;
        }
        
    }

    

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("I up");
    }

    public void OnInputDown(InputEventData eventData)
    {
        locked = true;
        Debug.Log("I down: Lock");
    }


}
