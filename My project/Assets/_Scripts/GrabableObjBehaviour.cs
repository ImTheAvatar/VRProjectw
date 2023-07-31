using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class GrabableObjBehaviour : NetworkBehaviour
{
    public Transform attached;
    public Vector3 offset;
    public Transform parent;
    public GrabableObjRange ObjInRange;
    Rigidbody rb;
    private async void Awake()
    {
        rb = GetComponent<Rigidbody>();
        offset = Vector3.zero;
        await Task.Delay(1000);
        rb.isKinematic = true;
    }
    private void Update()
    {
        rb.isKinematic = true;
        if (parent != null)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                transform.parent = parent;
            }
        }

        if (attached == null) return;
        transform.position = attached.position+offset;
    }
    public void TurnOffRB()
    {
    }
    public void TurnOnRB()
    {
        if(ObjInRange.go!=null)
        {
            transform.parent=ObjInRange.go.transform;
            Debug.Log("attaching");
            return;
        }
    }
}
