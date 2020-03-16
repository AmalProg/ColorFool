using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour
{
	public List<Ability> Abilities { get { return _abilities; } set { _abilities = value; Init (); } }
    public Player Player { get { return _player; } set { _player = value; } }

    public GameObject _cooldownUIPrefab;
    private Player _player;
	private List<Ability> _abilities;
    private List<GameObject> _cdUIObjs;

    void Awake() {
        _cdUIObjs = new List<GameObject>();
        _player.SwitchFormEvent.AddListener(UpdateCooldownUI);
        _abilities = _player.Abilities;
        Init();
    }

	void Start() {
    }

	void Update() {
	}

    private void UpdateCooldownUI() {
        for(int i = 0; i < _cdUIObjs.Count; i++) {
            if(i == _player.FormValue * Player.ABILITIES_PER_FORM + i % Player.ABILITIES_PER_FORM) {
                _cdUIObjs[i].SetActive(true);
            } else {
                _cdUIObjs[i].SetActive(false);
            }
        }
    }

    void Init() {
		float startXPos = Screen.width * 1 / 8 + _cooldownUIPrefab.GetComponent<RectTransform>().sizeDelta.x;
		float width = Screen.width * 3 / 4;
		float spacing = width / Player.ABILITIES_PER_FORM;
		float yPos = Screen.height * 1 / 6;

        _cdUIObjs.Clear();
        for (int i = 0; i < _abilities.Count; i++) {
            // every Player.ABILITIES_PER_FORM ui position is reset
            _cdUIObjs.Add(Instantiate (_cooldownUIPrefab, 
                    new Vector3 (startXPos + (i % Player.ABILITIES_PER_FORM) * spacing, yPos), 
                    new Quaternion (), 
                    transform));
			CooldownUI cdUI = _cdUIObjs[i].GetComponent<CooldownUI> ();
			cdUI.Ability = _abilities[i];
		}
        UpdateCooldownUI();
    }
}

