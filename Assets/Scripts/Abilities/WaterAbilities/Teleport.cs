using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Queue<GameObject> teleports { get { return _teleports; } }

    private static Queue<GameObject> _teleports = new Queue<GameObject>(); // each teleport on the map

    // Start is called before the first frame update
    void Awake()
    {
        _teleports.Enqueue(this.gameObject);

        if (_teleports.Count > 2) { // only 2 teleport on the map at a time
            GameObject.Destroy(_teleports.Dequeue()); // Destroy the first spawned teleport
        }
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetButton("Action")) {
            foreach (GameObject teleport in _teleports) {
                if (!teleport.Equals(this.gameObject)) {
                    other.transform.position = teleport.GetComponent<Transform>().position;

                    Teleport.Clear();
                    break;
                }
            }
        }
    }

    private static void Clear() { // destroy all teleports on the map
        while(_teleports.Count > 0) { 
            GameObject.Destroy(_teleports.Dequeue());
        }
    }
}
