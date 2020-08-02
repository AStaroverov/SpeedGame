using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject carObj;

    private Car carComponent;

    private void Start() {
        carComponent = carObj.GetComponent<Car>();
    }

    void FixedUpdate() {
        carComponent.motorTorque = 1500 * Input.GetAxis("Vertical");
        carComponent.steeringAngle = 20 * Input.GetAxis("Horizontal");
    }
}
