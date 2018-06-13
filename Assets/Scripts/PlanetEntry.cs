using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetEntry : MonoBehaviour {

    public string sceneName;

	void Start () {
		
	}

    private void EnablePlayerEntry(GameObject go)
    {
        VehicleCockpit vc = go.GetComponentInChildren<VehicleCockpit>();
        PlayerController pc = go.GetComponent<PlayerController>();

        if (pc == null && vc != null)
        {
            pc = vc.GetPlayer();
        }

        if (pc != null)
        {
            pc.EnablePlanetEntry(this);
        }
    }

    private void DisablePlayerEntry(GameObject go)
    {
        VehicleCockpit vc = go.GetComponentInChildren<VehicleCockpit>();
        PlayerController pc = go.GetComponent<PlayerController>();

        if (pc == null && vc != null)
        {
            pc = vc.GetPlayer();
        }

        if (pc != null)
        {
            pc.DisablePlanetEntry();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnablePlayerEntry(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        DisablePlayerEntry(other.gameObject);
    }
}
