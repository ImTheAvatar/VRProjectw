using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class GrabableObjBehaviour : NetworkBehaviour
{
    public GrabableObjRange ObjInRange;
    Rigidbody rb;
    public int ParentId;
    public int ObjectId;
    public ObjectData data=null;
    private async void Awake()
    {
        rb = GetComponent<Rigidbody>();
        await Task.Delay(1000);
        rb.isKinematic = true;
        data=InputManager.Instance.objectsLocalPosition.GetData(ParentId).data.FirstOrDefault(a => a.id == ObjectId);
        Debug.Log(data.position);
    }
    private void Update()
    {
        rb.isKinematic = true;
    }
}
