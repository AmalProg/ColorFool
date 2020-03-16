using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlameThrower : Ability {

    private FlameThrowerTrigerrable _trigger;

    private float _castTime;

    public FireFlameThrower(Player user) : base(user) {
        _cooldown = 5.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.75f;

        _trigger = _user.GetComponent<FlameThrowerTrigerrable>();
    }

    override protected IEnumerator InternalUse() {
        yield return new WaitForSeconds(_castTime); // sleep for castTime

        _trigger.Trigger();
    }
}
