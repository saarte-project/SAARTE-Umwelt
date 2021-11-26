// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.XR.WSA;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    /// <summary>
    /// This class demonstrates clearing spatial observations.
    /// </summary>
    [AddComponentMenu("Scripts/MRTK/Examples/ClearSpatialObservations")]
    public class ClearSpatialObservations : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether observations are to be cleared (true) or if the observer is to be resumed (false).
        /// </summary>
        private bool clearObservations = true;
        //
        private GameObject basePoint;

        /// <summary>
        /// Toggles the state of the observers.
        /// </summary>
        public void ToggleObservers()
        {
            var spatialAwarenessSystem = CoreServices.SpatialAwarenessSystem;
            if (spatialAwarenessSystem != null)
            {
                if (clearObservations)
                {
                    
                    spatialAwarenessSystem.SuspendObservers();
                    spatialAwarenessSystem.ClearObservations();
                    clearObservations = false;
                    //
                    //basePoint = GameObject.Find("BasePosition");
                    //
                    //DestroyImmediate(basePoint);
                }
                else
                {
                    spatialAwarenessSystem.ResumeObservers();
                    clearObservations = true;
                }
            }
        }
    }
}