using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using colorFoolCombatInterfaces;
using System.Timers;

public class MonoBehaviourDoTZone : MonoBehaviourAbility
{
    public float _damagePerSec; // damages dealt per second
    protected List<IDamageable> _entityToDamage; // entity which are inside the hitbox
    protected float _damageTickPerSec; // number of tick per second
    protected System.Timers.Timer _damageTimer;

    // Start is called before the first frame update
    protected void Awake()
    {
        _entityToDamage = new List<IDamageable>();

        _damageTimer = new System.Timers.Timer(1000 / _damageTickPerSec); // damage enemys _damageTickPerSec
        _damageTimer.Elapsed += new ElapsedEventHandler(InflictDamages);
        _damageTimer.AutoReset = true;
        _damageTimer.Enabled = true;
    }

    protected void OnTriggerEnter(Collider other) {
        _entityToDamage.Add(other.GetComponent<IDamageable>()); // add entity to deal damage to
    }

    protected void OnTriggerExit(Collider other) {
        _entityToDamage.Remove(other.GetComponent<IDamageable>()); // remove entity to deal damage to
    }

    protected void InflictDamages(object sender, ElapsedEventArgs e) {
        foreach (IDamageable entity in _entityToDamage) { // deal damage to each entity in the area
            entity.TakeDamage(_damagePerSec / _damageTickPerSec);
        }
    }

    new private void OnEnable() {
        base.OnEnable();

        _damageTimer.Start();
    }

    new protected void OnDisable() {
        base.OnDisable();

        _damageTimer.Stop();
        _entityToDamage.Clear(); // retire toutes les entités
    }
}
