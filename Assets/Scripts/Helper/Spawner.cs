using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class Spawner : MonoBehaviour {
	public GameObject obj;

	public float timeBetweenSpawn = 5;

	private float timeSinceLastSpawn = 0;

	// Use this for initialization
	void Start () {
		GameObject go = Instantiate(obj, transform.position, Quaternion.identity);
		go.GetComponent<MovementAI>().target = Globals.GetPlayer().GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > timeBetweenSpawn) {
			EnemyData ed = EnemyFactory.getInstance().getEnemy(1, Random.Range(0.9f, 1.1f));
			GameObject go = EnemyFactory.getInstance().InstantiateEnemy(ed, transform.position, Quaternion.identity);
			go.GetComponent<MovementAI>().target = Globals.GetPlayer().GetComponent<Rigidbody2D>();
			timeSinceLastSpawn = 0;
		}
	}
}
