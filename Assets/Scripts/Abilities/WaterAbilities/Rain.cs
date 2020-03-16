using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;
using System.Timers;

public class Rain : MonoBehaviourDoTZone {

    public float _radius;

    public GameObject _image;
    private CapsuleCollider _capsuleCollider;
    private ParticleSystem _particleSystem;
    private ParticleSystem.ShapeModule _particleSystemShapeModule;

    new void Awake() {
        _effectTime = 5.0f;
        _damageTickPerSec = 4f;
        _damagePerSec = 20.0f;

        base.Awake();
    }

    // Start is called before the first frame update
    void Start() {
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystemShapeModule = _particleSystem.shape;

        SetRadius(2.0f);

        Disable(); // wait first init before disabling
    }

    private void SetRadius(float r) {
        _image.transform.localScale *= _radius / _image.transform.localScale.x;
        _radius = r;
        _capsuleCollider.radius = _radius;
        _particleSystemShapeModule.radius = _radius;
    }    
}
