using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Awake()
    {
        PlayerManager.PM.SetPlayerRespawn(transform.position);
        PlayerManager.PM.RespawnPlayer();
    }
}
