using UnityEngine;
using System.Collections;

public class WaterPathTrigerrable : MonoBehaviour {

    public GameObject _prefab;
    public Transform _spawn;

    public void Trigger() {
        GameObject obj = UnityEngine.Object.Instantiate(_prefab,
            _spawn.position, transform.rotation);
    }
}
