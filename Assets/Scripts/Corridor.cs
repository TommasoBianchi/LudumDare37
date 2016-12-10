using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{

    public int width;
    public int height;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject floorNearWallPrefab;
    public GameObject wallTopPrefab;

    void Start()
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

        // Right
        GameObject wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
        wallTopTile.transform.localScale = new Vector3(0.1f, height - 0.5f, 1);
        wallTopTile.transform.localPosition = new Vector3(width - 0.55f, height / 2f + 0.2f, 0);
        // Left
        wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
        wallTopTile.transform.localScale = new Vector3(0.1f, height - 0.5f, 1);
        wallTopTile.transform.localPosition = new Vector3(-0.45f, height / 2f + 0.2f, 0);
    }
}
