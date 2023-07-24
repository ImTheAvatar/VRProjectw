using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool canInteract;
    [SerializeField] Transform HandPos;
    bool HandFull=>grabbed!=null;
    [SerializeField]GrabableObjBehaviour grabbed;
    public void Update()
    {
        if (!canInteract) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed "+HandFull+" "+grabbed);
            if (HandFull)
            {
                Debug.Log("hand full");
                grabbed.attached = null;
                grabbed = null;
            }
            else
            {
                Debug.Log("hand not full");
                if (Physics.Raycast
                    (Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit HitInfo, 5f))
                {
                    Debug.Log("RayCast hit " + HitInfo.collider.gameObject.name);
                    var go = HitInfo.collider.gameObject;
                    if (go.CompareTag("Grab"))
                    {
                        Debug.Log("Grabing");
                        var grabObj = go.GetComponent<GrabableObjBehaviour>();
                        grabObj.attached = HandPos;
                        grabbed= grabObj;
                    }
                }
            }
        }
    }
}
