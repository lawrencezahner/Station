using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserControls : MonoBehaviour {

    public float power;
    public float powerIncrease;

    public float yawPower;
    public float pitchPower;
    public float rollPower;

    [SerializeField]
    private float currentPower;
    [SerializeField]
    private float currentYaw;
    [SerializeField]
    private float currentPitch;
    [SerializeField]
    private float currentRoll;

    private VehicleCockpit vehicleCockpit;

    private void Start()
    {
        vehicleCockpit = GetComponentInChildren<VehicleCockpit>();
    }

    // Update is called once per frame
    void Update () {
        if (vehicleCockpit.hasPlayer)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentPower = Mathf.Clamp(currentPower + (powerIncrease * Time.deltaTime), 0, power);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                currentPower = Mathf.Clamp(currentPower - (powerIncrease * 2 * Time.deltaTime), 0, power);
            }

            if (currentPower > (power / 5))
            {
                var mouseY = Input.GetAxis("Mouse Y");
                var mouseX = Input.GetAxis("Mouse X");

                if (mouseY != 0)
                {
                    currentPitch += Time.deltaTime * pitchPower * Mathf.Sign(mouseY);
                }

                if (mouseX != 0)
                {
                    currentYaw += Time.deltaTime * yawPower * Mathf.Sign(mouseX);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    currentRoll -= rollPower * Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    currentRoll += rollPower * Time.deltaTime;
                }
            }

            if (currentPower <= (power / 5))
            {
                currentPitch = Mathf.Clamp(currentPitch - ((pitchPower / 4) * Time.deltaTime), 0, currentPitch);
                currentYaw = Mathf.Clamp(currentYaw - ((yawPower / 4) * Time.deltaTime), 0, currentYaw);
            }

            if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            {
                currentRoll = Mathf.Clamp(Mathf.Abs(currentRoll) - ((rollPower / 3) * Time.deltaTime), 0, Mathf.Abs(currentRoll)) * Mathf.Sign(currentRoll);
            }

            var pitch = currentPitch * (1 / (currentPower + 1));
            var yaw = currentYaw * (1 / (currentPower + 1));
            var roll = currentRoll * (1 / (currentPower + 1));

            transform.rotation = transform.rotation * Quaternion.Euler(pitch, yaw, roll);
            transform.Translate(transform.forward * currentPower * Time.deltaTime, Space.World);
        }
	}
}
