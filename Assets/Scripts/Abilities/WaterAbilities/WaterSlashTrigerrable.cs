using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlashTrigerrable : MonoBehaviour
{
    public GameObject waterSlashPrefab;
    public Transform bulletSpawn;

    public void Trigger() {
        GameObject waterSlashObj = UnityEngine.Object.Instantiate(waterSlashPrefab, bulletSpawn.position,
                transform.rotation);
    }
}
