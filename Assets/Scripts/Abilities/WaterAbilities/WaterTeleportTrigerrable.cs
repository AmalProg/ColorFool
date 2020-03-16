using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTeleportTrigerrable : MonoBehaviour
{
    public GameObject _waterTeleportPrefab;
    public Transform _teleportSpawn;

    public void Trigger() {
        GameObject waterTeleportObj = UnityEngine.Object.Instantiate(_waterTeleportPrefab,
            _teleportSpawn.position, transform.rotation);
    }
}
