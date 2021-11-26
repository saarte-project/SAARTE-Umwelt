using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Swaps position of all leafes once they spawn. Seed for randomization is the iteration! 
/// </summary>
public class LeafRandomizer : MonoBehaviour
{

/// <summary>
/// Receives iteration count and runs randomization. 
/// </summary>
    private void Start()
    {
        int iteration = FindObjectOfType<SceneSelect>().sceneIteration;
        RandomizeLeafes(iteration);
    }

/// <summary>
/// Swaps position of all leafes once they spawn. Seed for randomization is the iteration! 
/// </summary>
    public void RandomizeLeafes(int seed)
    {
        Random.InitState(seed);

        int leafCount = transform.childCount;

        for(int i = 0; i < leafCount; i++)
        {
            int randomTargetIndex = Random.Range(0, leafCount);
            Vector3 oldPos = transform.GetChild(i).transform.position;
            Vector3 newPos = transform.GetChild(randomTargetIndex).transform.position;

            transform.GetChild(i).transform.position = newPos;
            transform.GetChild(randomTargetIndex).transform.position = oldPos;
        }
    }
}
