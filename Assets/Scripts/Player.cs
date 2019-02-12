using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;


// 26.565051177078  angle due to player movement

public class Player : Entity, IDamageable, IHealable {

    public float Life { get { return _life; } }
    public float MaxLife { get { return _maxLife; } }
    public bool IsDead { get { return _isDead; } }
    public bool UsingMoveAbility { get { return _usingMoveAbility; } set { _usingMoveAbility = value; } }

    private Animator _anim;
    private BoxCollider _collider;

    public Dictionary<string, float> _baseStats;
    public float _life;
    public float _maxLife;
    public float _speed;
    private bool _isDead;
    private Vector3 _lastPosition;
    private Vector3 _direction;
    private uint _state; // identifiant de la forme dans laquelle

    private Ability[] _abilities;
    private bool _usingMoveAbility;

    protected void Awake() {
        _baseStats = new Dictionary<string, float>();
        _baseStats.Add("_speed", 7f);
        _baseStats.Add("_maxLife", 100f);

        _speed = _baseStats["_speed"];
        _maxLife = _baseStats["_maxLife"];
        _life = _baseStats["_maxLife"];

        _state = 0;

        _usingMoveAbility = false;
    }

    protected void Start() {
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponent<BoxCollider>();

        _abilities = new Ability[4];
        _abilities[0] = new Dash(this);
        _abilities[1] = new WaterSlash(this);
        _abilities[2] = new WaterTeleport(this);
        _abilities[3] = new WaterRain(this);

        _isDead = false;
    }

    protected void Update() {
        Timer.Update(Time.deltaTime);

        _lastPosition = transform.position;

        if (!_usingMoveAbility)
            Move();

        if (Input.GetButton("Space")) {
            if (_abilities[0 + 4 * _state].Use()) {
                _anim.SetTrigger("Dash");
            }
        }
        if (Input.GetButton("Spell1")) {
            if (_abilities[1 + 4 * _state].Use()) {
            } 
        }
        if (Input.GetButton("Spell2")) {
            if (_abilities[2 + 4 * _state].Use()) {
            }
        }
        if (Input.GetButton("Spell3")) {
            if (_abilities[3 + 4 * _state].Use()) {
            }
        }
    }

    protected void FixedUpdate() {
        HitBoxupdate();
    }

    protected void Move() {
        float elapsedTime = Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal") * _speed * elapsedTime;
        float vertical = Input.GetAxis("Vertical") * _speed * elapsedTime;

        transform.Translate(horizontal, 0, vertical, Space.World);

        if (horizontal != 0 || vertical != 0) {
            _direction = transform.position - _lastPosition;

            float angle = Vector3.SignedAngle(_direction, transform.forward, Vector3.up);
            if (angle < 0)
                transform.Rotate(0, Math.Min(-angle, 30), 0);
            else
                transform.Rotate(0, Math.Max(-angle, -30), 0);

            //transform.LookAt(transform.position + _direction);

            _anim.SetBool("Moving", true);
        } else {
            _anim.SetBool("Moving", false);
        }
    }

    private void HitBoxupdate() {
        _collider.center = new Vector3(_collider.center.x, _anim.GetFloat("DashHeight"), _collider.center.z);
    }

    public virtual void TakeDamage(float damage) {
		_life -= damage;

		if(_life <= 0) {
            _life = 0;

			Kill();
		}
	}

	public virtual void Heal(float heal) {
        _life += heal; 

		if (_life > _maxLife)
            _life = _maxLife;
	}

	public virtual void Kill () {
		_isDead = true;
	}

	void OnDestroy() {
	}
}