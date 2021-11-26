using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;

public class SetBasePositionSAM : MonoBehaviour, IMixedRealityInputHandler
{

    private GameObject basePoint;
    private GameObject baseRoot;

    public float MinHeight = 1.0f;

    private bool locked;
    private float _delayMoment;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SAM Casting script.");
        locked = false;
        _delayMoment = Time.time + 2;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hitInfo;

        GazeProvider gp = GetComponent<GazeProvider>();

        bool successful = Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hitInfo,
                20.0f,
                Physics.DefaultRaycastLayers);

        if (Time.time > _delayMoment)
        {
            if (successful & locked)
            {
                float difference = gp.transform.position.y - hitInfo.point.y;


                if (difference > MinHeight)//zu testen?
                {

                    Debug.Log("Hit at (for base position):" + hitInfo.point);
                    Debug.Log("GazeProvider at (for base position):" + gp.transform.position);
                    Debug.Log("Difference:" + difference);

                    basePoint = GameObject.Find("BasePosition");

                    basePoint.transform.localPosition = hitInfo.point;
                    Vector3 temp = new Vector3(0, 0, 0.3f);
                    basePoint.transform.position += temp;

                    Renderer[] bt = basePoint.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in bt)
                        r.enabled = true;
                    locked = false;
                   //check for better place for the world anchor, implement also the removing of it!
                    //basePoint.AddComponent<WorldAnchor>();
                }
                else
                {
                    Debug.Log("Look at the floor and try again!");
                    Debug.Log("Difference:" + difference);

                    basePoint = GameObject.Find("BasePosition");
                    Renderer[] rt = basePoint.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in rt)
                        r.enabled = false;

                }
            }
        }
        
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("I up");
    }

    public void anchoring()
    {
        basePoint.AddComponent<WorldAnchor>();
    }

    public void OnInputDown(InputEventData eventData)
    {
        locked = true;
        Debug.Log("I down: Lock");
    }    

    private void OnEnable()
    {
        // Instruct Input System that we would like to receive all input events of type
        // IMixedRealitySourceStateHandler and IMixedRealityHandJointHandler
        // CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(this);
       CoreServices.InputSystem?.RegisterHandler<IMixedRealityInputHandler>(this);
    }

    private void OnDisable()
    {
        // This component is being destroyed
        // Instruct the Input System to disregard us for input event handling
        //CoreServices.InputSystem?.UnregisterHandler<IMixedRealitySourceStateHandler>(this);
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityInputHandler>(this);
    }
}
