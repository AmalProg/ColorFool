using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;

public class SlashProjectile : MonoBehaviour
{
    private int _damage;
    public float _speed;
    protected Vector3 _direction;
    private float _lifeTime;

    public Vector3 direction { get { return _direction; } set { _direction = value; _direction.Normalize(); } }

    protected void Awake() {
        _speed = 15.0f;
        _damage = 10;
        _direction = transform.forward;
    }

    public void Start() {
        _lifeTime = 2.0f;
        Invoke("Disable", _lifeTime);
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
                damageable.TakeDamage(_damage);
            }
        }
        Destroy(this.gameObject); // destruction whatever is hit
    }

    private void Disable() {
        Destroy(this.gameObject);
    }
}
