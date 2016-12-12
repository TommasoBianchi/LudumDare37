using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Sprite openedDoorSprite;

    public Room room;
    public Room linkedRoom;

    public bool hubDoor;
    public bool doorToHub;

    void Start()
    {
        if (doorToHub)
        {
            linkedRoom = Hub.instance;
        }
    }

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = openedDoorSprite;
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GetComponent<Collider2D>().enabled = hubDoor;

        FadeScreen.Animate(2, ChangeRoom);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GetComponent<Collider2D>().enabled = hubDoor;

        FadeScreen.Animate(2, ChangeRoom);
    }

    void ChangeRoom()
    {
        if (linkedRoom != null)
        {
            if (!doorToHub)
            {
                linkedRoom.Generate();
                linkedRoom.transform.position = (hubDoor) ? Vector3.up * 100 : transform.position;
            }

            if (linkedRoom.bottomDoor != null)
                Globals.GetPlayerController().transform.position = linkedRoom.bottomDoor.transform.position + Vector3.up * 1f;
            else
                Globals.GetPlayerController().transform.position = linkedRoom.topDoor.transform.position - Vector3.up * 2;

            if (!doorToHub)
                linkedRoom.StartRoom();
            else // prepare new first room if going back to the hub
            {
                Destroy(room.topDoor.gameObject);
                linkedRoom.topDoor.linkedRoom = (Instantiate(room.roomPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Room>();
                linkedRoom.topDoor.linkedRoom.roomPrefab = room.roomPrefab;
                Globals.CurrentLevel = 0;
                Globals.GetPlayerController().addLife(Globals.GetPlayerController().MaxLife - Globals.GetPlayerController().Life);
            }

            if (room != null)
                Destroy(room.gameObject);
        }
    }
}
