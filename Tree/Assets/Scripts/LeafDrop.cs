using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Respawns leaves at their initial position (+ small random offset) if they fall under the ground.
/// </summary>
public class LeafDrop : MonoBehaviour
{

    Vector3 initialPosition;

    float estimatedGroundLevel;     // rough estimate, +/- 2 meter tolerance
    float fallTolerance = 5;        // if leaf falls X meter under ground tolerance its position will bet set back to initial position

    bool checkForRespawn = true;

/// <summary>
/// Stores initial position of the leaf, estimates ground height and runs required coroutine.
/// </summary>
    void Start()
    {
        initialPosition = transform.position;

        estimatedGroundLevel = Camera.main.transform.position.y - 1.7f;

        StartCoroutine(RespawnCheck());
    }


/// <summary>
/// Checks if leaf dropped below the ground. If so, respawns it with slight random offset.
/// </summary>
    IEnumerator RespawnCheck()
    {
        while (checkForRespawn)
        {
            if (transform.position.y < estimatedGroundLevel - fallTolerance)
            {
                float randomXDist = (float)(Random.Range(-4, 4)) * 0.1f;
                float randomZDist = (float)(Random.Range(-4, 4)) * 0.1f;

                transform.position = new Vector3(transform.position.x + randomXDist, initialPosition.y, transform.position.z + randomZDist);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
