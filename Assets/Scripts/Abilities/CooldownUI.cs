using System;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
	public Ability Ability { get { return _ability; } set { _ability = value; } }

	private Ability _ability;
	private Image _reloadImage;

	void Awake() {
		_reloadImage = GetComponentsInChildren<Image> () [1];
	}

	void Start() {

	}

	void Update() {
		_reloadImage.fillAmount = 1.0f - Math.Min(_ability.ElapsedTime / _ability.Cooldown, 1.0f);
	}
}

