using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientable : MonoBehaviour {

    public GameObject objectRoot;

    public void Orient(Transform parent)
    {
        if (objectRoot != null)
        {
            objectRoot.transform.parent = parent;
        }
        else
        {
            transform.parent = parent;
        }
    }
}
