using System.Collections;
using UnityEngine;

public class WaterSlash : Ability {

    private WaterSlashTrigerrable _trigger;

    private readonly float _castTime;

    public WaterSlash(Player user) : base(user) {
        _cooldown = 1.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.2f;

        _trigger = _user.GetComponent<WaterSlashTrigerrable>();
    }

    override protected IEnumerator InternalUse() {
        Vector3 direction = _user.transform.forward;

        yield return new WaitForSeconds(_castTime); // sleep for castTime

        _trigger.Trigger();
    }
}
