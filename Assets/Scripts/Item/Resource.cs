using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item {

	public ResourceType type;

	public void OnPickup() {
		Globals.GetPlayerController().AddResource(this);
	}
}
