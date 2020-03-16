using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player _player;

    public AbilitiesUI _abilitiesUI;

    // Start is called before the first frame update
    void Awake()
    {
        _abilitiesUI.Player = _player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
