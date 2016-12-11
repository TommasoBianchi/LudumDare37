using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

	public float after = 5;

	private float timeElapsed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed > after) {
			Destroy(gameObject);
		}
	}
}
