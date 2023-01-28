using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CarComponentsController : MonoBehaviour
{    
    [SerializeField] public Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "FreeRide")
        {
           gameObject.GetComponent<Checkpoints>().enabled = false;           
        }
    }
    
    void Update()
    {
        
    }
}
