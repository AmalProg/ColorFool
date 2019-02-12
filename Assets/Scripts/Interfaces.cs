using UnityEngine;

namespace colorFoolCombatInterfaces {
	public interface IDamageable {
		void TakeDamage(float d);
		void Kill();
	}

	public interface IHealable {
        void Heal(float h);
	}
}