using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShieldTrigerrable : MonoBehaviour {
    
    public GameObject _airShieldObj; // pour le réutiliser, plutôt qu'instancier/ détruire

    void Awake() {
    }

    public void Trigger() {
        _airShieldObj.SetActive(true);
    }
}
