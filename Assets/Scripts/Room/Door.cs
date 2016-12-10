using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Sprite openedDoorSprite;

    public Room linkedRoom;

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = openedDoorSprite;
        GetComponent<Collider2D>().isTrigger = true;

        if (linkedRoom != null)
        {
            linkedRoom.Generate();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        FadeScreen.instance.Animate(4, () =>
        {
            Debug.Log("hey");
            if (linkedRoom != null)
                FindObjectOfType<PlayerController>().transform.position = linkedRoom.bottomDoor.transform.position + Vector3.up * 2;
        });
    }
}
