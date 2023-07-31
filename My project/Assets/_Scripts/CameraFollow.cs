using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow :MonoBehaviour
{
    [SerializeField] float minViewDistance = 50f;
    [SerializeField] Transform playerBody;
    float xRotation = 0f;
    public float mouseSensitivity = 100f;
    Rigidbody body;
    private void Awake()
    {
        PlayerNetwork.onLocalPlayerSpawned += OnLocalPlayerSpawned;
    }

    private void OnLocalPlayerSpawned(PlayerNetwork network)
    {
        ChangeFollowObject(network.gameObject);
    }

    public void ChangeFollowObject(GameObject go)
    {
        body=go.GetComponent<Rigidbody>();
        transform.parent = go.transform;
        transform.localPosition = new Vector3(0, 0, 0);
        playerBody = go.transform;
    }
    private void Update()
    {
        if (playerBody == null) return;
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
