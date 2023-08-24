using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObjRange : MonoBehaviour
{
    public GrabableObjBehaviour go;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            go= other.gameObject.GetComponent<GrabableObjBehaviour>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            if (go != null)
            {
                if (go.gameObject == other.gameObject)
                {
                    go = null;
                }
            }
        }
    }
}
