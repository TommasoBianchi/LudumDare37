using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZEqualsY : MonoBehaviour {

    public bool updateEveryFrame;

	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
	}
	
	void Update () {
		if(updateEveryFrame)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
	}
}
