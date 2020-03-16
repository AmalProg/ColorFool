using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using colorFoolCombatInterfaces;


// 26.565051177078  angle due to player movement

enum Form { Water = 0, Air, Fire, Earth };

public class Player : Entity, IDamageable, IHealable {

    public static int ABILITIES_PER_FORM = 4;

    public List<Ability> Abilities { get { return _abilities; } }
    public List<Ability> CurrentAbilities { get { return _abilities.GetRange(1 + ABILITIES_PER_FORM * (int)_form, ABILITIES_PER_FORM); } }
    public UnityEvent SwitchFormEvent { get { return _switchFormEvent; } }
    public int FormValue { get { return (int)_form; } }

    private BoxCollider _collider;
    private SkinnedMeshRenderer _meshRenderer;

    public Dictionary<string, float> _baseStats;
    private Vector3 _lastPosition;
    private Vector3 _direction;
    private Form _form; // identifiant de la forme actuelle
    private Timer _transformationTimer;
    private float _transformationCooldown;
    private List<Ability> _abilities;

    private UnityEvent _switchFormEvent;

    protected void Awake() {
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _baseStats = new Dictionary<string, float> {
            { "_speed", 7f },
            { "_maxLife", 100f }
        };

        _speed = _baseStats["_speed"];
        _maxLife = _baseStats["_maxLife"];
        _life = _baseStats["_maxLife"];
        _shield = null;

        _transformationCooldown = 1.0f;
        _transformationTimer = new Timer(_transformationCooldown);
        _transformationTimer.SetAutoReset(false);
        _transformationTimer.AddTime(_transformationCooldown);

        SwitchForm(Form.Water);

        _abilities = new List<Ability> {
            new Dash(this), 
            new WaterSlash(this),
            new WaterTeleport(this),
            new WaterRain(this),

            new AirShield(this),
            new WaterSlash(this),
            new WaterRain(this),
            new WaterRain(this),

            new AirShield(this),
            new FireFlameThrower(this),
            new WaterRain(this),
            new WaterRain(this)

            // new RockBall(this);
            // new RockWall(this);
        };

        _usingMoveAbility = false;
        _shielded = false;
        _isDead = false;

        _switchFormEvent = new UnityEvent();
    }

    protected void Start() {
    }

    protected void Update() {
        Timer.Update(Time.deltaTime);

        _lastPosition = transform.position;

        if (!_usingMoveAbility)
            Move();
        Rotate();

        // offset dû à la valeur de la forme actuelle
        int formOffset = ABILITIES_PER_FORM * (int)_form;
        if (Input.GetButton("Space")) {
            if (_abilities[0 + formOffset].Use()) {
            }
        }
        if (Input.GetButton("Spell1")) {
            if (_abilities[1 + formOffset].Use()) {
            }
        }
        if (Input.GetButton("Spell2")) {
            if (_abilities[2 + formOffset].Use()) {
            }
        }
        if (Input.GetButton("Spell3")) {
            if (_abilities[3 + formOffset].Use()) {
            }
        }

        if (Input.GetButton("Form1")) {
            SwitchForm(Form.Water);
        } else if (Input.GetButton("Form2")) {
            SwitchForm(Form.Air);
        } else if (Input.GetButton("Form3")) {
            SwitchForm(Form.Fire);
        } else if (Input.GetButton("Form4")) {
            SwitchForm(Form.Earth);
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

        if (horizontal != 0 || vertical != 0) { // only if we move
            _anim.SetBool("Moving", true);
        } else {
            _anim.SetBool("Moving", false);
        }
    }

    private void Rotate() {
        // create a ray with the mouse position to hit the first object seen from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // add mask to only hit the direction plane object
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("DirectionPlane"))) {
            _direction = new Vector3(
            hit.point.x - transform.position.x,
            0,
            hit.point.z - transform.position.z).normalized;
        }

        // fast rotate in moving direction
        float angle = Vector3.SignedAngle(_direction, transform.forward, Vector3.up);
        if (angle < 0)
            transform.Rotate(0, Math.Min(-angle, 30), 0);
        else
            transform.Rotate(0, Math.Max(-angle, -30), 0);
    }

    private void SwitchForm(Form form) {
        bool switched = false;

        if (_transformationTimer.Check()) {
            if (form == Form.Water && _form != Form.Water) {
                _meshRenderer.material.color = new Color32(115, 215, 250, 110);

                switched = true;
            } else if (form == Form.Fire && _form != Form.Fire) {
                _meshRenderer.material.color = new Color32(235, 110, 45, 110);

                switched = true;
            } else if (form == Form.Air && _form != Form.Air) {
                _meshRenderer.material.color = new Color32(215, 215, 215, 110);

                switched = true;
            } else if (form == Form.Earth && _form != Form.Earth) {
                _meshRenderer.material.color = new Color32(115, 60, 15, 110);

                switched = true;
            }

            if (switched) {
                _transformationTimer.Reset();
                _form = form;
                _switchFormEvent.Invoke();
            }
        }
    }

    private void HitBoxupdate() {
        _collider.center = new Vector3(_collider.center.x, 0.5f + _anim.GetFloat("DashHeight"), _collider.center.z);
    }

    public virtual void TakeDamage(float damage) {
        // the shield absorbs even if damage is supérior to it's value
        if (_shielded)
            _shield.TakeDamage(damage);
        else 
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