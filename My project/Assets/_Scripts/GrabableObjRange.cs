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
            Debug.Log("these two are in collider " + go.name + " " + gameObject.name); ;
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
                    Debug.Log("these two are no longer in collider " + go.name + " " + gameObject.name);
                }
            }
        }
    }
}
