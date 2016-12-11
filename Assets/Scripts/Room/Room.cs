using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int ID { get; private set; }
    public int width;
    public int height;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject floorNearWallPrefab;
    public GameObject wallTopPrefab;
    public GameObject doorPrefab;

	private RoomPlan RoomPlan;

    public Door topDoor { get; private set; }
    public Door bottomDoor { get; private set; }

    private static bool firstRoom = true;
    private bool doorsLocked = true;

    void Start () 
    {
        if (firstRoom)
        {
            Generate();
            firstRoom = false;
        }
	}

    //public void Generate()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            GameObject floorTile = Instantiate((y == height - 1) ? floorNearWallPrefab : floorPrefab, transform) as GameObject;
    //            floorTile.transform.localPosition = new Vector3(x, y, 0);
    //        }
    //    }

    //    int topDoorPositionX = Random.Range(1, width - 1);
    //    int bottomDoorPositionX = Random.Range(1, width - 1);

    //    for (int x = 0; x < width; x++)
    //    {
    //        GameObject wallTile = Instantiate((x == topDoorPositionX) ? doorPrefab : wallPrefab, transform) as GameObject;
    //        wallTile.transform.localPosition = new Vector3(x, height, 0);
    //        if (x == topDoorPositionX)
    //            topDoor = wallTile.GetComponent<Door>();

    //        wallTile = Instantiate((x == bottomDoorPositionX) ? doorPrefab : wallPrefab, transform) as GameObject;
    //        wallTile.transform.localPosition = new Vector3(x, -0.65f, 0);
    //        if (x == bottomDoorPositionX)
    //            bottomDoor = wallTile.GetComponent<Door>();
    //    }

    //    // Top
    //    GameObject wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
    //    wallTopTile.transform.localScale = new Vector3(width, 0.1f, 1);
    //    wallTopTile.transform.localPosition = new Vector3(width / 2f - 0.5f, height + 0.5f, 0);
    //    // Bottom
    //    wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
    //    wallTopTile.transform.localScale = new Vector3(width, 0.1f, 1);
    //    wallTopTile.transform.localPosition = new Vector3(width / 2f - 0.5f, -0.1f, 0);
    //    // Right
    //    wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
    //    wallTopTile.transform.localScale = new Vector3(0.1f, height + 0.72f, 1);
    //    wallTopTile.transform.localPosition = new Vector3(width - 0.55f, height / 2f + 0.19f, 0);
    //    // Left
    //    wallTopTile = Instantiate(wallTopPrefab, transform) as GameObject;
    //    wallTopTile.transform.localScale = new Vector3(0.1f, height + 0.72f, 1);
    //    wallTopTile.transform.localPosition = new Vector3(-0.45f, height / 2f + 0.19f, 0);

    //    // Grab room plan
    //    RoomPlan = RoomPlanFactory.getInstance().getRoomPlan(this.ID);
    //    RoomPlan.GeneratePlan();
    //}

    public void Generate()
    {
        bool[,] map = new bool[width, height];

        FillMap(map);

        for (int i = 0; i < 5; i++)
        {
            map = CellularAutomataPass(map);
        }

        RemoveUnconnectedParts(map);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y])
                {
                    InstantiateFloorTile(map, x, y);
                }
            }
        }

        RoomPlan = RoomPlanFactory.getInstance().getRoomPlan(this.ID, this);
        RoomPlan.GeneratePlan();

        Globals.currentLevel ++;
    }

    private void RemoveUnconnectedParts(bool[,] map)
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);
        bool[,] visitedTiles = new bool[w, h];
        List<List<Vector2>> rooms = new List<List<Vector2>>();
        List<Vector2> biggestRoom = new List<Vector2>();

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (visitedTiles[x, y] == true || map[x, y] == false)
                    continue;

                List<Vector2> room = new List<Vector2>();
                Stack<Vector2> tileStack = new Stack<Vector2>();
                tileStack.Push(new Vector2(x, y));

                while (tileStack.Count > 0)
                {
                    Vector2 tile = tileStack.Pop();
                    visitedTiles[(int)tile.x, (int)tile.y] = true;

                    if (tile.x > 0 && visitedTiles[(int)tile.x - 1, (int)tile.y] == false && map[(int)tile.x - 1, (int)tile.y] == true)
                        tileStack.Push(new Vector2(tile.x - 1, tile.y));
                    if (tile.x < w - 1 && visitedTiles[(int)tile.x + 1, (int)tile.y] == false && map[(int)tile.x + 1, (int)tile.y] == true)
                        tileStack.Push(new Vector2(tile.x + 1, tile.y));
                    if (tile.y > 0 && visitedTiles[(int)tile.x, (int)tile.y - 1] == false && map[(int)tile.x, (int)tile.y - 1] == true)
                        tileStack.Push(new Vector2(tile.x, tile.y - 1));
                    if (tile.y < h - 1 && visitedTiles[(int)tile.x, (int)tile.y + 1] == false && map[(int)tile.x, (int)tile.y + 1] == true)
                        tileStack.Push(new Vector2(tile.x, tile.y + 1));

                    room.Add(tile);
                }

                rooms.Add(room);
                if (room.Count > biggestRoom.Count)
                    biggestRoom = room;
            }
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i] != biggestRoom)
            {
                for (int j = 0; j < rooms[i].Count; j++)
                {
                    map[(int)rooms[i][j].x, (int)rooms[i][j].y] = false;
                }
            }
        }
    }

    private void InstantiateFloorTile(bool[,] map, int x, int y)
    {
        GameObject floor;

        // Top wall
        if (y == height - 1 || map[x, y + 1] == false)
        {
            floor = Instantiate(floorNearWallPrefab, new Vector3(x, y, 0), Quaternion.identity, transform) as GameObject;
            GameObject wall = Instantiate(wallPrefab, new Vector3(x, y + 1, 0), Quaternion.identity, transform) as GameObject;

            // Right wall
            if (x == width - 1 || map[x + 1, y] == false)
            {
                GameObject topWall = Instantiate(wallTopPrefab, wall.transform) as GameObject;
                topWall.transform.localPosition = new Vector3(0.45f, 0, 0);
                topWall.transform.localScale = new Vector3(0.1f, 1, 1);
            }

            // Left wall
            if (x == 0 || map[x - 1, y] == false)
            {
                GameObject topWall = Instantiate(wallTopPrefab, wall.transform) as GameObject;
                topWall.transform.localPosition = new Vector3(-0.45f, 0, 0);
                topWall.transform.localScale = new Vector3(0.1f, 1, 1);
            }

            // Top door 
            bool okForDoor = true;
            for (int i = -2; i <= 2; i++)
            {
                okForDoor = okForDoor && x + i < width && x + i >= 0 && map[x + i, y] == true && (y == height - 1 || map[x + i, y + 1] == false);
            }
            if (okForDoor && topDoor == null)
            {
                Destroy(wall);
                GameObject door = Instantiate(doorPrefab, new Vector3(x, y + 1, 0), Quaternion.identity, transform) as GameObject;
                topDoor = door.GetComponent<Door>();
            }                
        }
        else
        {
            floor = Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity, transform) as GameObject;
        }

        // Bottom wall
        if (y == 0 || map[x, y - 1] == false)
        {
            GameObject wall = Instantiate(wallPrefab, new Vector3(x, y - 1, 0), Quaternion.identity, transform) as GameObject;


            // Right wall
            if (x < width - 1 && y > 0 && map[x + 1, y - 1] == true)
            {
                GameObject topWall = Instantiate(wallTopPrefab, wall.transform) as GameObject;
                topWall.transform.localPosition = new Vector3(0.55f, 0, 0);
                topWall.transform.localScale = new Vector3(0.1f, 1, 1);
            }

            // Left wall
            if (x > 0 && y > 0 && map[x - 1, y - 1] == true)
            {
                GameObject topWall = Instantiate(wallTopPrefab, wall.transform) as GameObject;
                topWall.transform.localPosition = new Vector3(-0.55f, 0, 0);
                topWall.transform.localScale = new Vector3(0.1f, 1, 1);
            }

            // Bottom door 
            bool okForDoor = true;
            for (int i = -2; i <= 2; i++)
            {
                okForDoor = okForDoor && x + i < width && x + i >= 0 && map[x + i, y] == true && (y == 0 || map[x + i, y - 1] == false);
            }
            if (okForDoor && bottomDoor == null)
            {
                Destroy(wall);
                GameObject door = Instantiate(doorPrefab, new Vector3(x, y - 1, 0), Quaternion.identity, transform) as GameObject;
                bottomDoor = door.GetComponent<Door>();
            }  
        }

        // Right wall
        if (x == width - 1 || (map[x + 1, y] == false && (y == 0 || map[x + 1, y - 1] == false) && (y == height - 1 || map[x + 1, y + 1] == false)))
        {
            GameObject topWall = Instantiate(wallTopPrefab, floor.transform) as GameObject;
            topWall.transform.localPosition = new Vector3(0.45f, 0, 0);
            topWall.transform.localScale = new Vector3(0.1f, 1, 1);
        }

        // Left wall
        if (x == 0 || (map[x - 1, y] == false && (y == 0 || map[x - 1, y - 1] == false) && (y == height - 1 || map[x - 1, y + 1] == false)))
        {
            GameObject topWall = Instantiate(wallTopPrefab, floor.transform) as GameObject;
            topWall.transform.localPosition = new Vector3(-0.45f, 0, 0);
            topWall.transform.localScale = new Vector3(0.1f, 1, 1);
        }

        /*// Adjust corners 'cause it has to be perfect!
        // Top right
        if ((x == width - 1 && y == height - 1) || (x < width - 1 && y < height - 1 && map[x + 1, y + 1] == false))
        {
            GameObject topWall = Instantiate(wallTopPrefab, floor.transform) as GameObject;
            topWall.transform.localPosition = new Vector3(0.45f, 0.45f, 0);
            topWall.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }

        // Top left
        if ((x == 0 && y == height - 1) || (x > 0 && y < height - 1 && map[x - 1, y + 1] == false))
        {
            GameObject topWall = Instantiate(wallTopPrefab, floor.transform) as GameObject;
            topWall.transform.localPosition = new Vector3(-0.45f, 0.45f, 0);
            topWall.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }

        // Bottom right
        if ((x == width - 1 && y == 0) || (x < width - 1 && y > 0 && map[x + 1, y - 1] == false))
        {
            GameObject topWall = Instantiate(wallTopPrefab, floor.transform) as GameObject;
            topWall.transform.localPosition = new Vector3(0.45f, -0.45f, 0);
            topWall.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }

        // Bottom left
        if ((x == 0 && y == 0) || (x > 0 && y > 0 && map[x - 1, y - 1] == false))
        {
            GameObject topWall = Instantiate(wallTopPrefab, floor.transform) as GameObject;
            topWall.transform.localPosition = new Vector3(-0.45f, -0.45f, 0);
            topWall.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }*/
    }

    private bool[,] CellularAutomataPass(bool[,] map)
    {
        bool[,] newMap = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourCount = 0;
                int filledNeighbours = 0;

                for (int h = -1; h <= 1; h++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if ((h != 0 || k != 0) && x + h >= 0 && x + h < width && y + k >= 0 && y + k < height)
                        {
                            neighbourCount++;
                            if (map[x + h, y + k])
                                filledNeighbours++;
                        }
                    }
                }

                float fillAmount = filledNeighbours / (float)neighbourCount;
                if (fillAmount > 0.55f)
                    newMap[x, y] = true;
                else if (fillAmount < 0.4f)
                    newMap[x, y] = false;
                else
                    newMap[x, y] = map[x, y];
            }
        }

        return newMap;
    }

    private void FillMap(bool[,] map)
    {
        float radius = Mathf.Max(width, height);
        Vector2 center = new Vector2(width / 2f, height / 2f);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = Random.Range(0, radius) * Random.Range(0, radius) > (new Vector2(x, y) - center).sqrMagnitude;
            }
        }
    }

	public void Update() {
        RoomPlan.UpdatePlan();
        if (false && RoomPlan.IsCleared() && doorsLocked) {
            // Instantiate next room
            topDoor.linkedRoom = (Instantiate(transform.gameObject, transform.position + Vector3.up * 100, Quaternion.identity) as GameObject).GetComponent<Room>();
            topDoor.linkedRoom.ID = this.ID + 1;

            Debug.Log("Doors unlocked");
            UnlockDoors();
            doorsLocked = false;
        }
	}

	public void UnlockDoors() {
        topDoor.Open();
        bottomDoor.Open();
	}
}
