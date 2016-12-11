using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpData {

	public int type; // attack == 0; speed == 1; none == 2;
	public float Duration;
	public float mult;

	abstract public void OnStart();

	abstract public void OnFinish();

	abstract public void OnAttack();
}
