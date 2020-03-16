using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float Life { get { return _life; } }
    public float MaxLife { get { return _maxLife; } }
    public float Speed { get { return _speed; } }
    public Shield Shield { set { _shield = value; } }
    public bool IsDead { get { return _isDead; } }
    public bool UsingMoveAbility { get { return _usingMoveAbility; } set { _usingMoveAbility = value; } }
    public bool Shielded { get { return _shielded; } set { _shielded = value; } }
    public Animator Anim { get { return _anim; } }

    public float _life;
    public float _maxLife;
    public float _speed;
    protected Shield _shield;
    protected bool _isDead;
    protected bool _usingMoveAbility;
    protected bool _shielded;
    protected Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
