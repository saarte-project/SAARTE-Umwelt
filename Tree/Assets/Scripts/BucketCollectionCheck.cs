using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to GameObject which defines the collision trigger for the bucket. Used to relay collision to BucketController.cs
/// </summary>
public class BucketCollectionCheck : MonoBehaviour
{
    [SerializeField]
    BucketController bucketController;

/// <summary>
/// Relays collision to BucketController.cs
/// </summary>
    private void OnTriggerEnter(Collider other)
    {
        bucketController.TriggerCollision(other);

    }
}
