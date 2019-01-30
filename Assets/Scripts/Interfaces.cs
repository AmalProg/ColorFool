using UnityEngine;

namespace colorFoolCombatInterfaces {
	public interface IDamageable {
		void TakeDamage(int d, Entity caster);
		void Kill(Entity caster);
	}

	public interface IHealable {
        void Heal(int h);
	}
}