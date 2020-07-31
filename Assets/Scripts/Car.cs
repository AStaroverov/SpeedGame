using UnityEngine;
using System.Collections.Generic;
    
public class Car : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float motorTorque = 0; // maximum torque the motor can apply to wheel
    public float steeringAngle = 0; // maximum steer angle the wheel can have
        
    public void FixedUpdate()
    {
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steeringAngle;
                axleInfo.rightWheel.steerAngle = steeringAngle;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motorTorque;
                axleInfo.rightWheel.motorTorque = motorTorque;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftTransform);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightTransform);
        }
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
    
        collider.GetWorldPose(out position, out rotation);
    
        trans.position = position;
        trans.rotation = rotation;
    }
}
    
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public Transform leftTransform;
    public Transform rightTransform;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}