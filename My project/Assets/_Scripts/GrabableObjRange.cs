using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObjRange : MonoBehaviour
{
    public GameObject go;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            go= other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            if (go != null)
            {
                if (go == other.gameObject)
                {
                    go = null;
                }
            }
        }
    }
}
