using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int moveSpeed;

    private bool controlEnabled;

    private new Rigidbody rigidbody;
    private float initialDrag;

    private bool gravityChanging;
    private bool gravityEnabled;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialDrag = rigidbody.drag;
        controlEnabled = true;
    }

    private void Update()
    {
        if (controlEnabled)
        {
            if (Input.GetAxis("FB") != 0)
            {
                Vector3 fbVector = transform.forward * moveSpeed * Input.GetAxis("FB") * Time.deltaTime;

                if (gravityEnabled)
                {
                    transform.Translate(fbVector, Space.World);
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        fbVector *= 2f;
                    }

                    rigidbody.AddForce(fbVector, ForceMode.Acceleration);
                }
            }

            if (Input.GetAxis("LR") != 0)
            {
                Vector3 lrVector = transform.right * moveSpeed * Input.GetAxis("LR") * Time.deltaTime;

                if (gravityEnabled)
                {
                    transform.Translate(lrVector, Space.World);
                }
                else
                {
                    rigidbody.AddForce(lrVector, ForceMode.Acceleration);
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (gravityEnabled)
                {

                }
                else
                {
                    rigidbody.drag = initialDrag * 5;
                }
            }
            else
            {
                rigidbody.drag = initialDrag;
            }

            if (gravityChanging)
            {
                rigidbody.useGravity = gravityEnabled;
            }
        }
    }

    public void SwitchGravity(bool gravity)
    {
        gravityChanging = true;
        gravityEnabled = gravity;
    }

    public void EnableControls(bool enabled)
    {
        controlEnabled = enabled;
    }
}
