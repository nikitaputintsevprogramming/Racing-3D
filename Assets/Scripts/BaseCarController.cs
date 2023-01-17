using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}
[System.Serializable]
public class WheelParticles
{
    public ParticleSystem FRWheel;
    public ParticleSystem FLWheel;
    public ParticleSystem RRWheel;
    public ParticleSystem RLWheel;
}    
    


public class BaseCarController : MonoBehaviour
{    
    private Rigidbody playerRB;

    public WheelColliders wheelColliders;
    public WheelMeshes wheelMeshes;   
    public WheelParticles wheelParticles;    

    public float gasInput;
    public float brakeInput;
    public float steerInput;

    public float motorPower;
    public float brakePower;
    private float slipAngle;
    public float speed;
    public AnimationCurve steeringCurve;
    WheelHit[] wheelHits;


    public GameObject smokePrefab;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        wheelHits = new WheelHit[4];
        InstantiateSmoke();
    }
    void InstantiateSmoke()
    {
        wheelParticles.FRWheel = Instantiate(smokePrefab, wheelColliders.FRWheel.transform.position - Vector3.up * wheelColliders.FRWheel.radius, 
        Quaternion.identity, wheelColliders.FRWheel.transform).GetComponent<ParticleSystem>();

        wheelParticles.FLWheel = Instantiate(smokePrefab, wheelColliders.FLWheel.transform.position - Vector3.up * wheelColliders.FLWheel.radius, 
        Quaternion.identity, wheelColliders.FLWheel.transform).GetComponent<ParticleSystem>();

        wheelParticles.RRWheel = Instantiate(smokePrefab, wheelColliders.RRWheel.transform.position - Vector3.up * wheelColliders.RRWheel.radius, 
        Quaternion.identity, wheelColliders.RRWheel.transform).GetComponent<ParticleSystem>();

        wheelParticles.RLWheel = Instantiate(smokePrefab, wheelColliders.RLWheel.transform.position - Vector3.up * wheelColliders.RLWheel.radius, 
        Quaternion.identity, wheelColliders.RLWheel.transform).GetComponent<ParticleSystem>();
    }

    public void FixedUpdate()
    {
        speed = playerRB.velocity.magnitude;
        CheckInput();
        ApplyMotor();
        ApplyBrake();
        ApplySteering();
        CheckParticles();
        ApplyWheelPosition();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
        if(movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        if(movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }
    }

    void ApplyBrake()
    {
        wheelColliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        wheelColliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;

        wheelColliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        wheelColliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
    }

    void ApplyMotor()
    {
        wheelColliders.RRWheel.motorTorque = motorPower * gasInput;
        wheelColliders.RLWheel.motorTorque = motorPower * gasInput;
    }

    void ApplySteering()
    {
        float steeringAngle = steerInput * steeringCurve.Evaluate(speed);

        // Подправляем дрифт боком
        steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);

        wheelColliders.FRWheel.steerAngle = steeringAngle;
        wheelColliders.FLWheel.steerAngle = steeringAngle;
    }

    void ApplyWheelPosition()
    {
        UpdateWheel(wheelColliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(wheelColliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(wheelColliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(wheelColliders.RLWheel, wheelMeshes.RLWheel);
    }

    void CheckParticles()
    {       
        wheelColliders.FRWheel.GetGroundHit(out wheelHits[0]);        
        wheelColliders.FLWheel.GetGroundHit(out wheelHits[1]);
        wheelColliders.RRWheel.GetGroundHit(out wheelHits[2]);
        wheelColliders.RLWheel.GetGroundHit(out wheelHits[3]);

        float slipAllowance = 0.5f;        
        
        if(Mathf.Abs(wheelHits[0].sidewaysSlip) + Mathf.Abs(wheelHits[0].forwardSlip) > slipAllowance)
        {
            wheelParticles.FRWheel.Play();
        }
        else
        {
            wheelParticles.FRWheel.Stop();
        }

        if(Mathf.Abs(wheelHits[1].sidewaysSlip) + Mathf.Abs(wheelHits[1].forwardSlip) > slipAllowance)
        {
            wheelParticles.FLWheel.Play();
        }
        else
        {
            wheelParticles.FLWheel.Stop();
        }

        if(Mathf.Abs(wheelHits[2].sidewaysSlip) + Mathf.Abs(wheelHits[2].forwardSlip) > slipAllowance)
        {
            wheelParticles.RRWheel.Play();
        }
        else
        {
            wheelParticles.RRWheel.Stop();
        }

        if(Mathf.Abs(wheelHits[3].sidewaysSlip) + Mathf.Abs(wheelHits[3].forwardSlip) > slipAllowance)
        {
            wheelParticles.RLWheel.Play();
        }
        else
        {
            wheelParticles.RLWheel.Stop();
        }
    }

    void UpdateWheel(WheelCollider wheelCollider, MeshRenderer wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = rotation;
    }
}


