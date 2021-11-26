using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUnderground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Scenes/RayCastTestScene_Alex");
        SceneManager.LoadScene("RayCastTestScene_Alex");
        Debug.Log("Bin in LoadUnderground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
