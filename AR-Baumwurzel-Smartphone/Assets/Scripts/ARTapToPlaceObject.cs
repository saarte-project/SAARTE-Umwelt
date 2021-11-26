using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public GameObject canvasUI;
    public GameObject objectClone;

    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool placementLock = false;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        canvasUI.SetActive(false);
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (!placementLock && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    public void ResetSession()
    {
        Destroy(objectClone);
        placementLock = false;
        canvasUI.SetActive(false);
    }

    private void PlaceObject()
    {
        Vector3 objectPose = placementPose.position;
        objectPose.y = objectPose.y - 0.6f;
        placementPose.position = objectPose;

        objectClone = (GameObject)Instantiate(objectToPlace, placementPose.position, placementPose.rotation);

        placementLock = true;
        canvasUI.SetActive(true);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid && !placementLock)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        //var screenCenter = new Vector3(0.5f, 0.5f); //TESTING
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
