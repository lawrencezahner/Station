using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationField : MonoBehaviour {

    public GameObject objectRoot;
    public GameObject orientObject;

    private void OnTriggerEnter(Collider other)
    {
        Orientable orientableObject = other.gameObject.GetComponent<Orientable>();
        
        if (orientableObject != null)
        {
            orientableObject.Orient(orientableObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Orientable orientableObject = other.gameObject.GetComponent<Orientable>();

        if (orientableObject != null)
        {
            if (objectRoot != null)
            {
                orientableObject.Orient(objectRoot.transform);
            }
            else
            {
                orientableObject.Orient(null);
            }
        }
    }
}
