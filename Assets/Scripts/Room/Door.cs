using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Sprite openedDoorSprite;

    public Room room;
    public Room linkedRoom;

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = openedDoorSprite;
        GetComponent<Collider2D>().isTrigger = true;

        if (linkedRoom != null)
        {
            linkedRoom.Generate();
            linkedRoom.transform.position = transform.position + Vector3.up * 100;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GetComponent<Collider2D>().enabled = false;

        FadeScreen.Animate(4, () =>
        {
            if (linkedRoom != null)
            {
                FindObjectOfType<PlayerController>().transform.position = linkedRoom.bottomDoor.transform.position + Vector3.up * 2;
                Destroy(room.gameObject);
                linkedRoom.StartRoom();
            }
        });
    }
}
