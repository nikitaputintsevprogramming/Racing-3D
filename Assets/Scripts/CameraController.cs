using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{    
    public Transform playerTransform;
    public Rigidbody playerRB;

    public Vector3 offset;
    public float speed;

    public void CreateCar()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerRB = playerTransform.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Vector3 playerForward = (playerRB.velocity + playerTransform.forward).normalized;
        transform.position = Vector3.Lerp(transform.position,
            playerTransform.position + playerTransform.TransformVector(offset) + playerForward * (-5f),
            speed * Time.deltaTime);
        transform.LookAt(playerTransform);
    }
}
