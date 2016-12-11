using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Speed;
    public float Range;

    private float distanceTravelled = 0;

	void Start () {
		
	}
	
	void Update () {
        float movement = Speed * Time.deltaTime;
        transform.Translate(Vector3.up * movement);
        distanceTravelled += movement;

        if (distanceTravelled > Range)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 8) { // enemy
            other.gameObject.GetComponent<Enemy>().Hit(this);
        }
    }
}
