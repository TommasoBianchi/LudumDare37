using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Sprite openedDoorSprite;

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = openedDoorSprite;
    }
}
