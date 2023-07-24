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
    bool HandFull => grabbed != null;
    [SerializeField] GrabableObjBehaviour grabbed;
    public override void OnNetworkSpawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (IsLocalPlayer)
        {
            onLocalPlayerSpawned?.Invoke(this);
        }
    }
    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;
        Run();
    }
    void Run()
    {
        if (moveInput.x == 0 && moveInput.y == 0) return;
        transform.position += (transform.forward * moveInput.y + transform.right * moveInput.x) * walkSpeed * Time.deltaTime;
    }
    public void OnMove(InputValue val)
    {
        moveInput = val.Get<Vector2>();
    }
    [ServerRpc(RequireOwnership = false)]
    public void AttachObjectServerRpc(Vector3 viewPoint,Vector3 forward)
    {
        if (HandFull)
        {
            grabbed.attached = null;
            grabbed = null;
        }
        else
        {
            if (Physics.Raycast
                (viewPoint, forward, out RaycastHit HitInfo, 5f))
            {
                var go = HitInfo.collider.gameObject;
                if (go.CompareTag("Grab"))
                {
                    var grabObj = go.GetComponent<GrabableObjBehaviour>();
                    grabObj.attached = HandPos;
                    grabbed = grabObj;
                }
            }
        }
    }
}