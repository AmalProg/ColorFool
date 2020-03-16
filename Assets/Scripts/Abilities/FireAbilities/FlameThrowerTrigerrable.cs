using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTrigerrable : MonoBehaviour {

    public GameObject _flameThrowerObj; // pour le réutiliser, plutôt qu'instancier/détruire
    public Transform _spawnPoint;

    private FlameThrower _script;

    void Awake() {
        _script = _flameThrowerObj.GetComponent<FlameThrower>();
        _flameThrowerObj.gameObject.transform.position = _spawnPoint.position;
    }

    public void Trigger() {
        _script.Play();
    }
}
