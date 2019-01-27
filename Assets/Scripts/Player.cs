using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;


// 26.565051177078  angle due to player movement

public class Player : MonoBehaviour, IDamageable, IHealable {

	public int Life { get { return _life; } }
	public int MaxLife { get { return _maxLife; } }
	public bool IsDead { get { return _isDead; } }
    public bool UsingMoveAbility { get { return _usingMoveAbility; } set { _usingMoveAbility = value; } }

    public Dictionary<string, float> _baseStats;
    protected int _life;
    protected int _maxLife;
    public float _speed;
	private bool _isDead;
    private Vector3 _lastPosition;
    private Vector3 _direction;

	private Ability[] _abilities;
    private bool _usingMoveAbility;

	protected void Awake() {
        _baseStats = new Dictionary<string, float>();
        _baseStats.Add("_speed", 7f);

        _speed = _baseStats["_speed"];

        _usingMoveAbility = false;

    }

	protected void Start() {
        _abilities = new Ability[4];
        _abilities[0] = new Dash(this);

		_isDead = false;
	}

	protected void Update() {
        Timer.Update(Time.deltaTime);

        _lastPosition = transform.position;

        if (!_usingMoveAbility)
            Move();

        if(Input.GetButton("Space")) {
            _abilities[0].Use();
        }
	}

    protected void Move() {
        float elapsedTime = Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal") * _speed * elapsedTime;
        float vertical = Input.GetAxis("Vertical") * _speed * elapsedTime;

        transform.Translate(horizontal, 0, vertical, Space.World);

        if (horizontal != 0 || vertical != 0) {
            _direction = transform.position - _lastPosition;
            transform.LookAt(transform.position + _direction);
        }
    }

	public virtual void TakeDamage(int damage, GameObject caster) {
		_life -= damage;

		if(_life <= 0) {
            _life = 0;

			Kill(caster);
		}
	}

	public virtual void Heal(int heal) {
        _life += heal; 

		if (_life > _maxLife)
            _life = _maxLife;
	}

	public virtual void Kill (GameObject caster) {
		_isDead = true;
	}

	void OnDestroy() {
	}
}