using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlash : Ability {
    
    private GameObject waterSlashPrefab;

    private float _castTime;

    public WaterSlash(Player user) : base(user) {
        _cooldown = 1.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.2f;

        waterSlashPrefab = Resources.Load("Prefab/WaterSlashPrefab") as GameObject;
    }

    override protected IEnumerator InternalUse() {
        Vector3 direction = _user.transform.forward;

        yield return new WaitForSeconds(_castTime); // sleep for castTime

        GameObject waterSlashObj = UnityEngine.Object.Instantiate(waterSlashPrefab,
                _user.transform.position + direction * (_user.transform.localScale.x + waterSlashPrefab.transform.localScale.x) / 1.5f,
                Quaternion.LookRotation(direction));
        waterSlashObj.GetComponentInChildren<WaterSlashProjectile>().user = _user;

        _user.UsingMoveAbility = false;
    }
}
