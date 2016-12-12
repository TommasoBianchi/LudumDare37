using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class Spawner : MonoBehaviour {
	public GameObject obj;

	public float timeBetweenSpawn = 5;

	private float timeSinceLastSpawn = 0;

	private Enemy parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > timeBetweenSpawn) {
			GameObject go = Instantiate(obj, transform.position, Quaternion.identity);
			//EnemyFactory.getInstance().InstantiateEnemy(obj, transform.position, Quaternion.identity);
			go.transform.parent = GameObject.FindGameObjectWithTag("Room").transform;
			go.GetComponent<Enemy>().Life = parent.MaxLife/4;
			go.GetComponent<Enemy>().MaxLife = parent.MaxLife/4;
			go.GetComponent<MovementAI>().target = Globals.GetPlayer().GetComponent<Rigidbody2D>();
			timeSinceLastSpawn = 0;
		}
	}
}
