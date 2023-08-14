using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class GrabableObjBehaviour : NetworkBehaviour
{
    public GrabableObjRange ObjInRange;
    Rigidbody rb;
    private async void Awake()
    {
        rb = GetComponent<Rigidbody>();
        await Task.Delay(1000);
        rb.isKinematic = true;
    }
    private void Update()
    {
        rb.isKinematic = true;
    }
}
