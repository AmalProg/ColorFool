using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;
using System.Timers;

public class Rain : MonoBehaviour {

    public float _radius;
    public float _damagePerSec;
    private List<IDamageable> _entityToDamage;
    private float _damageTickPerSec;
    private System.Timers.Timer _damageTimer;

    public GameObject _image;
    private CapsuleCollider _capsuleCollider;
    private ParticleSystem _particleSystem;
    private ParticleSystem.ShapeModule _particleSystemShapeModule;
    private float _effectTime;

    // Start is called before the first frame update
    void Start() {
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystemShapeModule = _particleSystem.shape;

        SetRadius(2.0f);
        _effectTime = 5.0f;
        _entityToDamage = new List<IDamageable>();
        _damageTickPerSec = 4f;
        _damagePerSec = 20.0f;

        _damageTimer = new System.Timers.Timer(1000 / _damageTickPerSec); // damage enemys _damageTickPerSec
        _damageTimer.Elapsed += new ElapsedEventHandler(InflictDamages);
        _damageTimer.AutoReset = true;
        _damageTimer.Enabled = true;

        Disable(); // wait first init before disabling
    }

    void OnEnable() {
        Invoke("Disable", _effectTime);
    }

    private void OnDisable() {
        CancelInvoke("Disable"); // cancel problem due to an early disable
    }

    private void OnTriggerEnter(Collider other) {
        _entityToDamage.Add(other.GetComponent<IDamageable>()); // remove entity to deal damage to
    }

    private void OnTriggerExit(Collider other) {
        _entityToDamage.Remove(other.GetComponent<IDamageable>()); // add entity to deal damage to
    }

    private void InflictDamages(object sender, ElapsedEventArgs e) {
        foreach(IDamageable entity in _entityToDamage) { // deal damage to each entity in the area
            entity.TakeDamage(_damagePerSec / _damageTickPerSec);
        }
    }

    private void SetRadius(float r) {
        _image.transform.localScale *= _radius / _image.transform.localScale.x;
        _radius = r;
        _capsuleCollider.radius = _radius;
        _particleSystemShapeModule.radius = _radius;
    }

    private void Disable() {
        gameObject.SetActive(false);
    }

    
}
