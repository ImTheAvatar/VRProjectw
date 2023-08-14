using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class ParentHandler : NetworkBehaviour
{
    public List<GrabableObjBehaviour> grabbed;
    public Transform FollowObj;
    public Vector3 offset;
    private void Awake()
    {
    }
    public void MakeParent(GrabableObjBehaviour g)
    {
        if (grabbed==null)
        {
            grabbed= new List<GrabableObjBehaviour>();
        }
        g.transform.parent = transform;
        grabbed.Add(g);
    }
    private void Update()
    {
        if(FollowObj != null)
        {
            transform.position= FollowObj.position+offset;
        }
    }
    public void Attach()
    {
        FollowObj = null;
        for(int i=0;i<grabbed.Count;i++)
        {
            var other = grabbed[i].ObjInRange.go;
            if (other != null)
            {
                var otherParent = grabbed[i].ObjInRange.go.transform.parent;
                if (otherParent != null)
                {
                    var otherParentHandler=otherParent.GetComponent<ParentHandler>();
                    if (otherParentHandler == this) continue;
                    var number = otherParentHandler.grabbed.Count;
                    for (int j=0;j<number;j++)
                    {
                        MakeParent(otherParentHandler.grabbed[j]);
                    }
                    otherParentHandler.grabbed.Clear();
                }
            }
        }
        Debug.Log("we have another " + grabbed.Count);
    }
}
