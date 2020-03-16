using UnityEngine;

public class Shield : MonoBehaviour {

    public Player _receiver;
    private float _effectTime;

    public float _value;
    private float _curValue;

    void Start() {
        _effectTime = 3.0f;
        
        _receiver.Shield = this;

        Disable(); // wait first init before disabling
    }

    public void TakeDamage(float damage) {
        _curValue -= damage;

        // shield break
        if (_curValue <= 0) {
            Disable();
        }
    }

    void OnEnable() {
        _curValue = _value;
        _receiver.Shielded = true;
        Invoke("Disable", _effectTime);
    }

    private void OnDisable() {
        CancelInvoke("Disable"); // cancel problem due to an early disable
    }

    private void Disable() {
        _receiver.Shielded = false;
        gameObject.SetActive(false);
    }


}
