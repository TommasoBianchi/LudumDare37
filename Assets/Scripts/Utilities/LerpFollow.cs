using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFollow : MonoBehaviour {

    public float smoothAmount;
    public Transform target;

	void Start () {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
	
	void Update () {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position, smoothAmount);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
	}
}
