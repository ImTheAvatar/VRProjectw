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
    InputManager inputManager;
    public override void OnNetworkSpawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (IsOwner)
        {
            inputManager = GetComponent<InputManager>();
            CameraFollow.Instance.ChangeFollowObject(gameObject);
            inputManager.canInteract = true;
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
}