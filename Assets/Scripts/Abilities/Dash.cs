﻿using System;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using UnityEngine;
using System.Collections;

public class Dash : Ability {

    private Timer _effectTimer;
    private Vector2 _userDirection;

    public Dash(Player user) : base(user) {
        _cooldown = 1.0f;
        _cooldownTimer.SetResetTime(_cooldown);
        _cooldownTimer.AddTime(_cooldown);

        _effectTimer = new Timer(0.3f);
    }

    override protected IEnumerator InternalUse() {
        _effectTimer.Reset();

        _user.UsingMoveAbility = true;

        Vector3 mousePos = Input.mousePosition;
        Vector2 centerPos = new Vector2(Screen.width / 2, Screen.height / 2);
        _userDirection = new Vector2(mousePos.x - centerPos.x, mousePos.y - centerPos.y);
        _userDirection.Normalize();

        while (!_effectTimer.Check()) {
            float elapsedTime = Time.deltaTime;
            float verTranslation = _userDirection.y * _user._speed * 10.0f * elapsedTime;
            float horTranslation = _userDirection.x * _user._speed * 10.0f * elapsedTime;

            _user.transform.Translate(horTranslation, 0, verTranslation, Space.World);

            yield return new WaitForSeconds(Time.deltaTime); // sleep for deltaTime time
        }

        _user.UsingMoveAbility = false;
    }
}