using System;
using System.Collections;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;


public abstract class Ability
{
    public bool offCooldown { get { return _offCooldown; } }

    protected Player _user;
    protected bool _offCooldown;
    protected float _cooldown;
    protected Timer _cooldownTimer;

    protected Ability(Player user) {
        _user = user;
        _offCooldown = true;
        _cooldownTimer = new Timer(_cooldown);
        _cooldownTimer.setAutoReset(true);
    }

    public bool Use() {
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

