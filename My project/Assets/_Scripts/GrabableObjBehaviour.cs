using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GrabableObjBehaviour : NetworkBehaviour
{
    public Transform attached;
    private void Update()
    {
        if (attached == null) return;
        transform.position = attached.position;
    }
}
