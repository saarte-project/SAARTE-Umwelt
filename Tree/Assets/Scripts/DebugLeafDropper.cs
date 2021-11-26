using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only used for debugging purposes to drop leafes into the bucket in the Game View.
/// </summary>
public class DebugLeafDropper : MonoBehaviour
{
    [SerializeField]
    GameObject leafToDrop;

    private void Start()
    {
        StartCoroutine(DropLeafes(10, 3));
    }

    IEnumerator DropLeafes(int amount, float delay)
    {
        yield return new WaitForSeconds(5);


        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);

            GameObject newLeafe = Instantiate(leafToDrop);
            newLeafe.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
        }
    }
}
