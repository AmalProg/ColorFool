using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
	private static List<Timer> timers = new List<Timer>();

	private float _timeElapsed;
	private float _resetTime;
	private bool _stopped;
	private bool _autoReset;

	public Timer(float resetTime) {
		timers.Add (this);

		Reset ();
		_resetTime = resetTime;
		_stopped = false;
		_autoReset = false;
	}

	public static void Update (float elapsedTime)
	{
		foreach (Timer t in timers) {
			if (!t._stopped)
				t._timeElapsed += elapsedTime;
		}
	}

	public void SetResetTime(float time) {
		_resetTime = time;
	}

	public void setAutoReset(bool auto) {
		_autoReset = auto;
	}

	public void AddTime(float time) {
		_timeElapsed += time;
	}

	public void Reset() {
		_timeElapsed = 0.0f;
	}

	public void Stop() {
		_stopped = true;
		Reset ();
	}

	public void Start() {
		_stopped = false;
	}

	public void Pause() {
		_stopped = true;
	}

	public bool Check() {
		if (_timeElapsed >= _resetTime) {
			if(_autoReset)
				Reset ();
			return true;
		}
		return false;
	}
}