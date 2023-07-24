using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerNetwork player;
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
            player.AttachObjectServerRpc(Camera.main.transform.position,Camera.main.transform.forward);
        }
    }
    private void OnDestroy()
    {

        PlayerNetwork.onLocalPlayerSpawned -= OnLocalPlayerSpawn;
    }
}
