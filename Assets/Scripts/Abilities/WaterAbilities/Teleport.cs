using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public static Queue<GameObject> Teleports { get { return _teleports; } }
    public static int TeleportsCount { get { return _teleports.Count; } }

    private static Queue<GameObject> _teleports = new Queue<GameObject>(); // each teleport on the map

    private int _index;

    void Awake()
    {
        _teleports.Enqueue(this.gameObject);

        if (_teleports.Count > 2) { // only 2 teleport on the map at a time
            GameObject.Destroy(_teleports.Dequeue()); // Destroy the first spawned teleport
        }

        _index = _teleports.Count - 1;
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetButton("Action") && other.gameObject.CompareTag("Player")) {
            foreach (GameObject teleport in _teleports) {
                if (!teleport.Equals(this.gameObject)) {
                    AbilityEventManager.waterTeleportStartEvent.Invoke(_index);
                    break;
                }
            }
        }
    }

    public static void Clear() { // destroy all teleports on the map
        while(_teleports.Count > 0) { 
            GameObject.Destroy(_teleports.Dequeue());
        }
        AbilityEventManager.waterPathResetEvent.Invoke();
    }
}
