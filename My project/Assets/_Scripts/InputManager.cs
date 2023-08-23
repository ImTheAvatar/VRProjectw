using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Voice.Unity;
using Unity.Netcode;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerNetwork player;
    [SerializeField] Recorder recorder;
    private void Awake()
    {
        PlayerNetwork.onLocalPlayerSpawned += OnLocalPlayerSpawn;
    }

    private void OnLocalPlayerSpawn(PlayerNetwork network)
    {
        player=network;
    }

    public void Update()
    {
        if (player == null) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Clicked");
            player.AttachObjectServerRpc(Camera.main.transform.position,Camera.main.transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("V Clicked");
            player.DettachObjectServerRpc(Camera.main.transform.position, Camera.main.transform.forward);
        }
        if (Input.GetKey(KeyCode.E))
        {
            //TO ASK : CHECK PLAYER HANDFULL
                player.ChangeGrabOffsetServerRpc(new Vector3(0, 2f * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
                //Debug.Log("key Q pressed");
                player.ChangeGrabOffsetServerRpc(new Vector3(0, -2f * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.R))
        {
            //Debug.Log("key R pressed");
            player.ChangeGrabRotationServerRpc(new Vector3(0,100,0));
        }
        if (Input.GetKey(KeyCode.M)){
            recorder.TransmitEnabled=!recorder.TransmitEnabled;
        }
    }
    private void OnDestroy()
    {

        PlayerNetwork.onLocalPlayerSpawned -= OnLocalPlayerSpawn;
    }
}
