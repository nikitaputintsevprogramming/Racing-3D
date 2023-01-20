using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{    
    private UI_Controller _UI_Controller;    

    public GameObject[] AllCheckpoints;

    // Список собранных чекпоинтов
    [SerializeField] static List<Vector3> collectedCheckpoints = new List<Vector3>();

    void Start()
    {
        _UI_Controller = FindObjectOfType<UI_Controller>();

        AllCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    void Update()
    {
        print(collectedCheckpoints.Count);
        print(AllCheckpoints.Length);
        if(collectedCheckpoints.Count == AllCheckpoints.Length)
        {
            _UI_Controller.AddLap();
            collectedCheckpoints.Clear();
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        // Если это чекпойнт и он не был собран, то мы добавляем его в список собранных чекпойнтов
        if(other.gameObject.tag == "Checkpoint" && !IsCheckpointCollected(other.gameObject.transform.position))
        {
            collectedCheckpoints.Add(other.gameObject.transform.position);    
            other.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);     
        }

        if(other.gameObject.tag == "Plane")
        {
            if(collectedCheckpoints.Count > 0)
            {
                transform.position = collectedCheckpoints[collectedCheckpoints.Count - 1];                 
            }
            else
            {
                SceneManager.LoadScene("Track");
            }            
        }

        if(other.gameObject.tag == "PlaceSettings")
        {
            SceneManager.LoadScene("Track");
        }
    } 

    // Проверяем был ли собран когда-нибудь чекпойнт после предыдущего
    bool IsCheckpointCollected(Vector3 checkingCheckpointPosition)
    {
        if(collectedCheckpoints.Count > 0)
        {
            foreach (Vector3 collectedCheckpoint in collectedCheckpoints)
            {
                if(checkingCheckpointPosition == collectedCheckpoint)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
