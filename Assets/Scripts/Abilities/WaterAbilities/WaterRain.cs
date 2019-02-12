using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRain : Ability {

    private WaterRainTrigerrable _trigger;

    private float _castTime;

    public WaterRain(Player user) : base(user) {
        _cooldown = 5.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.2f;

        _trigger = _user.GetComponent<WaterRainTrigerrable>();
    }

    override protected IEnumerator InternalUse() {
        yield return new WaitForSeconds(_castTime); // sleep for castTime

        _trigger.Trigger();
    }
}
