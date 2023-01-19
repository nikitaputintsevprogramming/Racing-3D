using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{    
    // Список собранных чекпоинтов
    [SerializeField] static List<Vector3> collectedCheckpoints = new List<Vector3>();


    public void OnTriggerEnter(Collider other) 
    {
        // Если это чекпойнт и он не был собран, то мы добавляем его в список собранных чекпойнтов
        if(other.gameObject.tag == "Checkpoint" && !IsCheckpointCollected(other.gameObject.transform.position))
        {
           collectedCheckpoints.Add(other.gameObject.transform.position);
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
