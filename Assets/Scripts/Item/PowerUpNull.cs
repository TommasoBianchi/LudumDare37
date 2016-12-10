using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpNull : PowerUp {

	public PowerUpNull() {
		this.Duration = 10;
	}

	public void OnPickup () {
		throw new System.NotImplementedException ();
	}

	public void OnStart() {
		throw new System.NotImplementedException ();
	}

	public void OnFinish() {
		throw new System.NotImplementedException ();
	}

	public void OnAttack() {
		throw new System.NotImplementedException ();
	}
}
