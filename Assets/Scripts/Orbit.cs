using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public GameObject center;
    public float orbitSpeed;

	// Use this for initialization
	void Start () {
        transform.LookAt(center.transform);	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.right * Time.deltaTime * orbitSpeed, Space.World);
        transform.LookAt(center.transform);
	}
}
