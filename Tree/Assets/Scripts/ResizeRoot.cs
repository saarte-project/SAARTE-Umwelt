using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;


/// <summary>
/// Called by speech command. Increases or decreases size of the root.
/// </summary>
public class ResizeRoot : MonoBehaviour
{

    private Vector3 scaleSize;
    public GameObject tree;



/// <summary>
/// Called by speech command. Increases root size by factor 1.1.
/// </summary>
    public void biggerSize()
    {

        tree.transform.localScale *= 1.1f;


    }

/// <summary>
/// Called by speech command. Decreases root size by factor 0.9.
/// </summary>
    public void smallerSize()
    {

        tree.transform.localScale *= 0.9f;


    }

}
