using UnityEngine;

public class FlameThrower : MonoBehaviourDoTZone {

    private ParticleSystem _particleSystem;
    private CapsuleCollider _collider;

    new private void Awake() {
        _effectTime = 2.0f;
        _damageTickPerSec = 30f;
        _damagePerSec = 30.0f;

        base.Awake();

        _collider = GetComponent<CapsuleCollider>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Start() {
        Disable(); // wait first init before disabling
    }

    public void Play() {
        _particleSystem.Play();
    }

    override protected void OnEnable() {
        _particleSystem.Stop();
    }

    override protected void Disable() {
    }
}
