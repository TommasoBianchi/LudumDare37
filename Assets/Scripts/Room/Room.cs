﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public int ID;
    public int width;
    public int height;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject floorNearWallPrefab;
    public GameObject wallTopPrefab;
    public GameObject doorPrefab;

	public RoomPlan RoomPlan;

    private Door topDoor;
    private Door bottomDoor;

    void Start () 
    {
        Generate();
	}

    void Generate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject floorTile = Instantiate((y == height - 1) ? floorNearWallPrefab : floorPrefab, transform) as GameObject;
                floorTile.transform.localPosition = new Vector3(x, y, 0);
            }
        }

        int topDoorPositionX = Random.Range(1, width - 1);
        int bottomDoorPositionX = Random.Range(1, width - 1);

        for (int x = 0; x < width; x++)
        {
            GameObject wallTile = Instantiate((x == topDoorPositionX) ? doorPrefab : wallPrefab, transform) as GameObject;
            wallTile.transform.localPosition = new Vector3(x, height, 0);
            if (x == topDoorPositionX)
                topDoor = wallTile.GetComponent<Door>();

            wallTile = Instantiate((x == bottomDoorPositionX) ? doorPrefab : wallPrefab, transform) as GameObject;
            wallTile.transform.localPosition = new Vector3(x, -0.65f, 0);
            if (x == bottomDoorPositionX)
                bottomDoor = wallTile.GetComponent<Door>();
        }

        // Top
        GameObject wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
        wallTopTile.transform.localScale = new Vector3(width, 0.1f, 1);
        wallTopTile.transform.localPosition = new Vector3(width / 2f - 0.5f, height + 0.5f, 0);
        // Bottom
        wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
        wallTopTile.transform.localScale = new Vector3(width, 0.1f, 1);
        wallTopTile.transform.localPosition = new Vector3(width / 2f - 0.5f, -0.1f, 0);
        // Right
        wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
        wallTopTile.transform.localScale = new Vector3(0.1f, height + 0.72f, 1);
        wallTopTile.transform.localPosition = new Vector3(width - 0.55f, height / 2f + 0.19f, 0);
        // Left
        wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
        wallTopTile.transform.localScale = new Vector3(0.1f, height + 0.72f, 1);
        wallTopTile.transform.localPosition = new Vector3(-0.45f, height / 2f + 0.19f, 0);
    }

	public void Update() {

	}

	public void UnlockDoors() {
        topDoor.Open();
        bottomDoor.Open();
	}
}
