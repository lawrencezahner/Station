using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static Transform lastPositionInSpace;

    public bool isInVehicle;

    private PlayerMovement playerMovement;
    private PlayerLook playerLook;
    private new Camera camera;

    private VehicleCockpit cockpit;
    [SerializeField]
    private bool canEnterVehicle;

    private PlanetEntry planetToEnter;
    [SerializeField]
    private bool canEnterPlanet;

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
                vehicle = hitinfo.collider.transform.parent.gameObject;
                    
                if (vehicle.GetComponentInChildren<VehicleCockpit>() != null && canEnterVehicle)
                {
                    EnterVehicle(vehicle);
                }
            }

            if (canEnterPlanet)
            {
                EnterPlanet();
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

        if (cockpit != null)
        {
            isInVehicle = true;
            canEnterVehicle = false;
            canEnterPlanet = false;
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
            canEnterPlanet = true;
            playerMovement.EnableControls(true);
            playerLook.EnableLook(true);
            camera.enabled = true;

            cockpit.UnloadPlayerFromVehicle();
            cockpit = null;
        }
    }

    public void EnterPlanet()
    {
        if (!isInVehicle)
        {
            lastPositionInSpace = transform;
            SceneManager.LoadScene(planetToEnter.sceneName);
        }
    }

    public void EnablePlanetEntry(PlanetEntry entry)
    {
        canEnterPlanet = true;
        planetToEnter = entry;
    }

    public void DisablePlanetEntry()
    {
        canEnterPlanet = false;
        planetToEnter = null;
    }
}
