using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private PlayerMovement playerMovement;
    private PlayerLook playerLook;
    private Camera camera;

    private VehicleCockpit cockpit;
    private bool canEnterVehicle;

    public bool isInVehicle;

    // Use this for initialization
    private void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        camera = GetComponent<Camera>();
        EnableGravity(false);

        canEnterVehicle = true;
        isInVehicle = false;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hitinfo;
            GameObject vehicle;

            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitinfo, 15f))
            {
                vehicle = hitinfo.collider.transform.parent.parent.gameObject;
                    
                if (vehicle.GetComponentInChildren<VehicleCockpit>() != null && canEnterVehicle)
                {
                    EnterVehicle(vehicle);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && isInVehicle)
        {
             ExitVehicle();
        }
    }

    public void EnableGravity(bool gravity)
    {
        playerMovement.SwitchGravity(gravity);
    }

    public void EnterVehicle(GameObject vehicle)
    {
        cockpit = vehicle.GetComponentInChildren<VehicleCockpit>();
        Debug.Log(cockpit);
        if (cockpit != null)
        {
            isInVehicle = true;
            canEnterVehicle = false;
            playerMovement.EnableControls(false);
            playerLook.EnableLook(false);
            camera.enabled = false;

            cockpit.LoadPlayerIntoVehicle(gameObject);
        }
    }

    public void ExitVehicle()
    {
        if (isInVehicle)
        {
            isInVehicle = false;
            canEnterVehicle = true;
            playerMovement.EnableControls(true);
            playerLook.EnableLook(true);
            camera.enabled = true;

            cockpit.UnloadPlayerFromVehicle();
            cockpit = null;
        }
    }
}
