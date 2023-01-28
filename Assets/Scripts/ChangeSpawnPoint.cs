using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnPoint : MonoBehaviour
{
    [SerializeField] public CameraController _cameraController;

    [SerializeField] public GameObject Car;
    [SerializeField] public GameObject StartSpawnPoint;



    void Start()
    {        
        // Car = GameObject.FindGameObjectWithTag("Player");
        Car = Resources.Load("Prefabs/car") as GameObject;
        StartSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        StartSpawn();
    }

    void StartSpawn()
    {
        Instantiate(Car, StartSpawnPoint.transform.position, StartSpawnPoint.gameObject.transform.rotation);
        _cameraController = FindObjectOfType<CameraController>();
        _cameraController.CreateCar();
    }
}
