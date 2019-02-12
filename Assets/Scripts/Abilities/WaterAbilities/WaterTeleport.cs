using System;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using UnityEngine;
using System.Collections;

public class WaterTeleport : Ability {

    private WaterTeleportTrigerrable _trigger;

    private float _castTime;

    public WaterTeleport(Player user) : base(user) {
        _cooldown = 1.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.1f;

        _trigger = _user.GetComponent<WaterTeleportTrigerrable>();
    }

    override protected IEnumerator InternalUse() {

        yield return new WaitForSeconds(_castTime); // sleep for castTime

        _trigger.Trigger();
    }
}