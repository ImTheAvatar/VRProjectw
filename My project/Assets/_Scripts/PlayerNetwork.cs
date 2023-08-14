using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] Vector2 moveInput;
    public static System.Action<PlayerNetwork> onLocalPlayerSpawned;
    public Transform HandPos;
    public bool HandFull => grabbed != null;
    public ParentHandler grabbed;
    public override void OnNetworkSpawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (IsLocalPlayer)
        {
            onLocalPlayerSpawned?.Invoke(this);
        }
        gameObject.name = OwnerClientId.ToString();
    }
    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;
        Run();
    }
    void Run()
    {
        if (moveInput.x == 0 && moveInput.y == 0) return;
        transform.position += Time.deltaTime * walkSpeed * (transform.forward * moveInput.y + transform.right * moveInput.x);
    }
    public void OnMove(InputValue val)
    {
        moveInput = val.Get<Vector2>();
    }
    [ServerRpc(RequireOwnership = false)]
    public void AttachObjectServerRpc(Vector3 viewPoint,Vector3 forward)
    {
        if (!IsServer) return;
        if (HandFull)
        {
            Debug.Log("Attaching");
            grabbed.Attach();
            grabbed = null;
        }
        else
        {
            Debug.Log("Grabing");
            if (Physics.Raycast
                (viewPoint, forward, out RaycastHit HitInfo, 5f))
            {
                var go = HitInfo.collider.gameObject;
                Debug.Log(go.name);
                if (go.CompareTag("Grab"))
                {
                    var grabObj = go.transform.parent.GetComponent<ParentHandler>();
                    grabObj.FollowObj = HandPos;
                    grabbed = grabObj;
                }
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void DettachObjectServerRpc(Vector3 viewPoint, Vector3 forward)
    {
        if (!IsServer) return;
        if (HandFull)
        {
            
        }
        else
        {
            Debug.Log("Detaching");
            if (Physics.Raycast
                (viewPoint, forward, out RaycastHit HitInfo, 5f))
            {
                var go = HitInfo.collider.gameObject;
                Debug.Log(go.name);
                if (go.CompareTag("Grab"))
                {
                    var grabObj = go.GetComponent<GrabableObjBehaviour>();
                    var prevParent = go.transform.parent.GetComponent<ParentHandler>();
                    prevParent.grabbed.Remove(grabObj);
                    var grabParent = Instantiate(GameManager.Instance.Prefab);
                    grabParent.GetComponent<NetworkObject>().Spawn();
                    grabParent.MakeParent(grabObj);
                    grabObj.transform.localPosition = Vector3.zero;
                    grabParent.FollowObj = HandPos;
                    grabbed = grabParent;
                }
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeGrabOffsetServerRpc(Vector3 v)
    {
        if(grabbed == null) { Debug.Log("cant ");return; }
        Debug.Log(grabbed.name + " changing height "+v);
        grabbed.offset += v;
    }
}