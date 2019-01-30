using System;
using UnityEngine;

public class AbilityMonoBehaviour : MonoBehaviour
{
	public Entity user { get { return _user; } set { _user = value; } }

	protected Entity _user;
}

