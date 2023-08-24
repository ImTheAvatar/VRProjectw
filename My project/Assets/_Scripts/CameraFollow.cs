using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow :MonoBehaviour
{
    [SerializeField] float minViewDistance = 70f;
    [SerializeField] Transform playerBody;
    float xRotation = 0f;
    public float mouseSensitivity = 100f;
    Rigidbody body;
    PlayerNetwork player;
    private void Awake()
    {
        PlayerNetwork.onLocalPlayerSpawned += OnLocalPlayerSpawned;
    }

    private void OnLocalPlayerSpawned(PlayerNetwork network)
    {
        ChangeFollowObject(network);
    }

    public void ChangeFollowObject(PlayerNetwork go)
    {
        player = go;
        body=go.gameObject.GetComponent<Rigidbody>();
        transform.parent = go.HeadPos;
        transform.localPosition = new Vector3(0, 0, 0);
        playerBody = go.transform;
    }
    private void Update()
    {
        if (playerBody == null) return;
        if (InputManager.Instance.IsVR)
        {
            transform.rotation=player.VRCamera.transform.rotation;
            player.Shape.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, minViewDistance);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
            //TO ASK : WHY:?
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }
    private void OnDestroy()
    {
        PlayerNetwork.onLocalPlayerSpawned -= OnLocalPlayerSpawned;
    }

}
