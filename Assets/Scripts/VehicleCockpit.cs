using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCockpit : MonoBehaviour {

    public Vector3 unloadPostion;
    public bool hasPlayer { get; private set; }

    private Camera camera;
    private GameObject player;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (hasPlayer)
        {
            player.transform.position = transform.position;
        }
    }

    public void LoadPlayerIntoVehicle(GameObject loadingPlayer)
    {
        player = loadingPlayer;
        camera.enabled = true;
        hasPlayer = true;
    }

    public void UnloadPlayerFromVehicle()
    {
        if (hasPlayer)
        {
            player.transform.position = transform.TransformPoint(transform.localPosition + unloadPostion);
            player = null;
            hasPlayer = false;
            camera.enabled = false;
        }
    }
}
