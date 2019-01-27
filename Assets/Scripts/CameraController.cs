using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject _player;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 12, _player.transform.position.z - 10);
    }
}
