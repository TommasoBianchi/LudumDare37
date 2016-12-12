using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class ExternalWall : MonoBehaviour {

	public GameObject enemy;
	private bool spawned = false;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == 10 && !spawned) { // if player
			Vector3 pos = new Vector3(Globals.GetPlayer().transform.position.x + 30, Globals.GetPlayer().transform.position.y + 30, Globals.GetPlayer().transform.position.z);
			GameObject go = Instantiate(enemy, pos, Quaternion.identity);
			go.GetComponent<MovementAI>().target = Globals.GetPlayer().GetComponent<Rigidbody2D>();

			spawned = true;
		}
	}
}
