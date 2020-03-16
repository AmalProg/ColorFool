using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRainTrigerrable : MonoBehaviour {
    public GameObject _waterRainPrefab;

    private GameObject _waterRainObj; // pour le réutiliser, plutôt qu'instancier/détruire

    void Awake() {
        _waterRainObj = UnityEngine.Object.Instantiate(_waterRainPrefab,
            new Vector3(0f, 0f, 0f), transform.rotation);
    }

    public void Trigger() {
        // create a ray with the mouse position to hit the first object seen from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // add mask to only hit map objects
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("Map"))) { 
            _waterRainObj.SetActive(true);
            _waterRainObj.transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
            _waterRainObj.transform.rotation = transform.rotation;
        }
    }
}
