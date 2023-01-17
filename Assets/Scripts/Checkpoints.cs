using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{    
    // Позиция последнего чекпойнта
    public Vector3 lastCheckpointPosition = Vector3.zero; 

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Checkpoint")
        {
            // Записываем позицию точки сохранения в переменную
            lastCheckpointPosition = other.gameObject.transform.position;
        }

        if(other.gameObject.tag == "Plane")
        {
            // Записываем позицию точки сохранения в переменную
            transform.position = lastCheckpointPosition;
        }
    } 
}
