using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public bool motor; // Приводят ли в движение наш автомобиль?
    public bool steering; // Будут ли поворачиать наш автомобиль?
}

public class BaseCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // информация о каждой индивидуальной оси
    public float maxMotorTorque; // макс. крутящий момент, который применяем к каждому колесу
    public float maxSteerAngle; // макс. угол выворота каждого колеса 

    // Поиск мешей (визуальных колес)    
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if(collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteerAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if(axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if(axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}



