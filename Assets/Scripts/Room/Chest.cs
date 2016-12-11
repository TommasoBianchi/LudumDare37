using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

	private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

	public void AddResource(ResourceType rt, int amount) {
		if (resources.ContainsKey(rt)) {
			resources[rt] += amount;
		}
		else {
			resources[rt] = amount;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { // player layer
			foreach (ResourceType rt in resources.Keys)
			{
				for (int i = 0; i < resources[rt]; i++) {
					Globals.GetPlayerController().AddResource(rt);
				}
			}
			Destroy(this.gameObject);
			GameObject text = Instantiate(Globals.GetPlayerController().Text, Globals.GetPlayer().transform.position, Quaternion.identity) as GameObject;
			text.transform.SetParent(GameObject.Find("OverlayCanvas").transform);
			string chestString = "";
			foreach (ResourceType rt in resources.Keys)
			{
				chestString += resources[rt] + " " + rt + ", ";
			}
			if (chestString.Length > 0) {
				chestString = chestString.Remove(chestString.Length - 1);
				chestString = chestString.Remove(chestString.Length - 1);
				text.GetComponent<Text>().color = Color.green;
			}
			else {
				chestString = "Empty :P";
				text.GetComponent<Text>().color = Color.yellow;
			}
			text.GetComponent<Text>().text = chestString;
			text.GetComponent<DestroyAfter>().after = 3.0f;
			text.GetComponent<MoveUp>().speed = 0.005f;
		}
	}
}
