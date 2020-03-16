using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourAbility : MonoBehaviour
{
    protected float _effectTime;

    virtual protected void OnEnable() {
        Invoke("Disable", _effectTime);
    }

    protected void OnDisable() {
        CancelInvoke("Disable"); // cancel problem due to an early disable
    }

    virtual protected void Disable() {
        gameObject.SetActive(false);
    }
}
