using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlashTrigerrable : MonoBehaviour
{
    public GameObject _prefab;
    public Transform _spawnPoint;
    protected Entity _user;

    public void Trigger() {
        GameObject obj = UnityEngine.Object.Instantiate(_prefab, _spawnPoint.position,
                transform.rotation);
        SlashProjectile script = obj.GetComponent<SlashProjectile>();
        script.User = _user;
    }
}
