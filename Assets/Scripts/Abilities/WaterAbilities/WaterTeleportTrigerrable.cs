using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTeleportTrigerrable : MonoBehaviour
{
    public GameObject waterTeleportPrefab;
    public Transform teleportSpawn;

    public void Trigger() {
        GameObject waterTeleportObj = UnityEngine.Object.Instantiate(waterTeleportPrefab,
            teleportSpawn.position, transform.rotation);
    }
}
