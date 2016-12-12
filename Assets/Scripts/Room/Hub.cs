using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : Room {

    public static Hub instance;

    public Door hubDoor;

	void Start () {
        instance = this;
        topDoor = hubDoor;
        ID = -1;
	}

    public override Vector3 ViewportToWorldPoint(Vector2 viewportPoint)
    {
        return topDoor.transform.position - Vector3.up * 2;
    }
}
