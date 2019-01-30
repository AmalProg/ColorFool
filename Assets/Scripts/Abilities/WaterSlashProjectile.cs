using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;

public class WaterSlashProjectile : AbilityMonoBehaviour
{
    private int _damage;
    public float _speed;
    protected Vector3 _direction;

    public Vector3 direction { get { return _direction; } set { _direction = value; _direction.Normalize(); } }

    protected void Awake() {
        _speed = 15.0f;
        _damage = 10;
        _direction = transform.forward;
    }

    public void Start() {

    }

    public void Update() {
        Move();
    }

    public void Move() {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter(Collider other) {
        if (other != null) {
            GameObject otherGameObject = other.gameObject;
            IDamageable damageable = otherGameObject.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.TakeDamage(_damage, _user);
            }
        }
        Destroy(this.transform.parent.gameObject); // destruction whatever is hit
    }
}
