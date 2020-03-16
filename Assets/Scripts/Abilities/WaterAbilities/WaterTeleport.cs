using System;
using UnityEngine;
using System.Collections;

public class WaterTeleport : Ability {

    private WaterTeleportTrigerrable _trigger;
    private WaterPathTrigerrable _triggerPath;
    private WaterPath _waterPath;

    private float _realCooldown;
    private float _inBetweenTeleportCooldown;
    private float _resetCooldown;
    private float _castTime;

    private bool _teleporting;

    public WaterTeleport(Player user) : base(user) {
        _realCooldown = 10.0f;
        _inBetweenTeleportCooldown = 1.0f;
        _cooldown = _realCooldown;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);
        _castTime = 0.1f;

        _teleporting = false;

        _trigger = _user.GetComponent<WaterTeleportTrigerrable>();
        _triggerPath = _user.GetComponent<WaterPathTrigerrable>();
        _waterPath = _user.GetComponentInChildren<WaterPath>();
        _waterPath.User = _user.gameObject;

        AbilityEventManager.waterTeleportStartEvent.AddListener(TeleportStart);
        AbilityEventManager.waterTeleportEndEvent.AddListener(TeleportEnd);
    }

    ~WaterTeleport() {
        AbilityEventManager.waterTeleportStartEvent.RemoveListener(TeleportStart);
        AbilityEventManager.waterTeleportEndEvent.RemoveListener(TeleportEnd);
    }

    override public bool Use() {
        _offCooldown = _cooldownTimer.Check();
        if (_offCooldown && Teleport.TeleportsCount < 2) {
            if (Teleport.TeleportsCount == 0) {
                _cooldown = _inBetweenTeleportCooldown;
                _cooldownTimer.SetResetTime(_cooldown);
                _user.StartCoroutine(SpawnLastTeleport());
                _waterPath.StartPath();
            } else if (Teleport.TeleportsCount == 1) {
                _cooldown = _realCooldown;
                _cooldownTimer.SetResetTime(_cooldown);
                _user.StartCoroutine(DespawnTeleport());
                _waterPath.StopPath();
            }

            _user.StartCoroutine(InternalUse());

            _offCooldown = false;
            return true;
        }
        return false;
    }

    private void TeleportStart(int from) {
        _teleporting = true;
    }

    private void TeleportEnd() {
        _teleporting = false;
        Teleport.Clear();
    }

    override protected IEnumerator InternalUse() {

        // sleep for castTime
        yield return new WaitForSeconds(_castTime); 

        _trigger.Trigger();
    }

    private IEnumerator DespawnTeleport() {

        // despawn teleports in 2/3 cooldown
        yield return new WaitForSeconds(_realCooldown * 2 / 3);

        if (!_teleporting) {
            Teleport.Clear();
        }
    }
    
    private IEnumerator SpawnLastTeleport() {

        // spawn lest teleport in 3 * inbetween cooldown
        yield return new WaitForSeconds(_inBetweenTeleportCooldown * 3);

        Use();
    }
}