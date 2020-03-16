using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShield : Ability {

    private AirShieldTrigerrable _trigger;

    private float _castTime;

    public AirShield(Player user) : base(user) {
        _cooldown = 5.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.2f;

        _trigger = _user.GetComponent<AirShieldTrigerrable>();
    }

    override public bool Use() {
        _offCooldown = _cooldownTimer.Check();
        if (_offCooldown && !_user.Shielded) {
            _user.StartCoroutine(InternalUse());

            _offCooldown = false;
            return true;
        }
        return false;
    }

    override protected IEnumerator InternalUse() {
        yield return new WaitForSeconds(_castTime); // sleep for castTime

        _trigger.Trigger();
    }
}
