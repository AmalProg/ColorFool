using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRainTrigerrable : MonoBehaviour {
    public GameObject _waterRainPrefab;

    private GameObject _waterTeleportObj; // pour le réutiliser, plutôt qu'instancier/détruire

    void Awake() {
        _waterTeleportObj = UnityEngine.Object.Instantiate(_waterRainPrefab,
            new Vector3(0f, 0f, 0f), transform.rotation);
    }

    public void Trigger() {
        // create a ray with the mouse position to hit the first object seen from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100.0f); // add mask to only hit map objects

        _waterTeleportObj.SetActive(true);
        _waterTeleportObj.transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
        _waterTeleportObj.transform.rotation = transform.rotation;
    }
}
