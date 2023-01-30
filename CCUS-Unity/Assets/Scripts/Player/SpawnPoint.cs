using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    PlayerManager pm;

    void Start()
    {
        pm = PlayerManager.PM;
        pm.SetPlayerRespawn(transform.position + (Vector3.up * pm.playerHeight));
        pm.RespawnPlayer();
    }
}
