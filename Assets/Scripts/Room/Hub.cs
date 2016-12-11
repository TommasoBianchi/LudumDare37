using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : Room {

    public static Hub instance;

    public Door hubDoor;

	void Start () {
        instance = this;
        topDoor = hubDoor;
	}
}
