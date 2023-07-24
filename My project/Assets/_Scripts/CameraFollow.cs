using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] float minViewDistance = 50f;
    [SerializeField] Transform playerBody;
    float xRotation = 0f;
    public float mouseSensitivity = 100f;
    public void ChangeFollowObject(GameObject go)
    {
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
        }
    }
}
