using System;
using System.Collections;
using UnityEngine;


public abstract class Ability
{
    public bool OffCooldown { get { return _offCooldown; } }
    public float Cooldown { get { return _cooldown;  } }
    public float ElapsedTime { get { return _cooldownTimer.ElapsedTime; } }

    protected Entity _user;
    protected Animator _userAnim;
    protected bool _offCooldown;
    protected float _cooldown;
    protected Timer _cooldownTimer;

    protected Ability(Entity user) {
        _user = user;
        _userAnim = _user.Anim;
        _offCooldown = true;
        _cooldownTimer = new Timer(_cooldown);
        _cooldownTimer.SetAutoReset(true);
    }

    public virtual bool Use() {
        _offCooldown = _cooldownTimer.Check();
        if (_offCooldown) {
            _user.StartCoroutine(InternalUse());

            _offCooldown = false;
            return true;
        }
        return false;
    }

    protected abstract IEnumerator InternalUse();
}

