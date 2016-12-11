using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {

    public int ID { get; private set; }
    public int width;
    public int height;

    public GameObject roomPrefab;
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

    private Vector2[,] nearestTiles;

    private Vector2 fallbackTopDoorPosition = -Vector2.one;
    private Vector2 fallbackBottomDoorPosition = -Vector2.one;

    void Start() 
    {
        if (firstRoom)
        {
            Generate();
            firstRoom = false;
            StartRoom();
        }
	}

    public void Generate()
    {
        bool[,] map = new bool[width, height];

        FillMap(map);

        for (int i = 0; i < 5; i++)
        {
            map = CellularAutomataPass(map);
        }

        RemoveUnconnectedParts(map);

        nearestTiles = new Vector2[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y])
                {
                    InstantiateFloorTile(map, x, y);
                    if (IsFloor(map, x, y + 1) && IsFloor(map, x, y - 1) && IsFloor(map, x + 1, y) && IsFloor(map, x - 1, y))
                        nearestTiles[x, y] = new Vector2(x, y);
                    else
                        nearestTiles[x, y] = -Vector2.one;
                }
                else
                {
                    nearestTiles[x, y] = -Vector2.one;
                }
            }
        }

        PrefillNearestTiles(map);

        if (topDoor == null)
        {
            GameObject door = Instantiate(doorPrefab, fallbackTopDoorPosition, Quaternion.identity, transform) as GameObject;
            topDoor = door.GetComponent<Door>();
            Debug.LogWarning("Top door in fallback position");
        }
        topDoor.room = this;

        if (bottomDoor == null)
        {
            GameObject door = Instantiate(doorPrefab, fallbackBottomDoorPosition, Quaternion.identity, transform) as GameObject;
            bottomDoor = door.GetComponent<Door>();
            Debug.LogWarning("Bottom door in fallback position");
        }
        bottomDoor.room = this;

        RoomPlan = RoomPlanFactory.getInstance().getRoomPlan(this.ID, this);
    }

    private bool IsFloor(bool[,] map, int x, int y)
    {
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1) && map[x, y] == true;
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
            for (int i = -1; i <= 1; i++)
            {
                okForDoor = okForDoor && x + i < width && x + i >= 0 && map[x + i, y] == true && (y == height - 1 || map[x + i, y + 1] == false);
            }
            if (okForDoor && topDoor == null)
            {
                Destroy(wall);
                GameObject door = Instantiate(doorPrefab, new Vector3(x, y + 1, 0), Quaternion.identity, transform) as GameObject;
                topDoor = door.GetComponent<Door>();
            }                

            // Fallback top door position (to use in case I'm not able to find a place where to spawn a door
            if (fallbackTopDoorPosition == -Vector2.one || y + 1 > fallbackTopDoorPosition.y)
                fallbackTopDoorPosition = new Vector2(x, y + 1);
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
            for (int i = -1; i <= 1; i++)
            {
                okForDoor = okForDoor && x + i < width && x + i >= 0 && map[x + i, y] == true && (y == 0 || map[x + i, y - 1] == false);
            }
            if (okForDoor && bottomDoor == null)
            {
                Destroy(wall);
                GameObject door = Instantiate(doorPrefab, new Vector3(x, y - 1, 0), Quaternion.identity, transform) as GameObject;
                bottomDoor = door.GetComponent<Door>();
            }

            // Fallback bottom door position (to use in case I'm not able to find a place where to spawn a door
            if (fallbackBottomDoorPosition == -Vector2.one || y - 1 < fallbackBottomDoorPosition.y)
                fallbackBottomDoorPosition = new Vector2(x, y - 1);
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

    private void PrefillNearestTiles(bool[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (nearestTiles[x, y] == -Vector2.one)
                {
                    // Find nearest tile
                    int minDistSqr = int.MaxValue;
                    Vector2 nearestTile = new Vector2(x, y);
                    for (int h = 0; h < width; h++)
                    {
                        for (int k = 0; k < height; k++)
                        {
                            if (map[h, k] == true)
                            {
                                int distSqr = (h - x) * (h - x) + (k - y) * (k - y);
                                if (distSqr > 0 && distSqr < minDistSqr)
                                {
                                    minDistSqr = distSqr;
                                    nearestTile = new Vector2(h, k);
                                }
                            }
                        }
                    }

                    nearestTiles[x, y] = nearestTile;
                }
            }
        }
    }

	public void Update() {
        RoomPlan.UpdatePlan();
        if (RoomPlan.IsCleared() && doorsLocked) {
            // Instantiate next room
            topDoor.linkedRoom = (Instantiate(roomPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Room>();
            topDoor.linkedRoom.roomPrefab = roomPrefab;
            topDoor.linkedRoom.ID = this.ID + 1;

            //Debug.Log("Doors unlocked");
            UnlockDoors();
            doorsLocked = false;

            // Change weapon
            Globals.GetPlayerController().WeaponData = WeaponFactory.getInstance().GetWeapon(Mathf.RoundToInt(ID / 3) + 1);
            GameObject text = Instantiate(Globals.GetPlayerController().Text, Globals.GetPlayer().transform.position, Quaternion.identity) as GameObject;
            text.transform.SetParent(GameObject.Find("OverlayCanvas").transform);
            string rollName = ("" + Globals.GetPlayerController().WeaponData.Roll).Equals("None") ? "" : ("" + Globals.GetPlayerController().WeaponData.Roll);
            text.GetComponent<Text>().text = rollName + " " + Globals.GetPlayerController().WeaponData.Type + " T" + Globals.GetPlayerController().WeaponData.Tier;
            text.GetComponent<DestroyAfter>().after = 3.0f;
            text.GetComponent<MoveUp>().speed = 0.005f;
        }
	}

	public void UnlockDoors() {
        topDoor.Open();
        bottomDoor.Open();
	}

    public Vector3 ViewportToWorldPoint(Vector2 viewportPoint)
    {
        viewportPoint.x = Mathf.Clamp01(viewportPoint.x);
        viewportPoint.y = Mathf.Clamp01(viewportPoint.y);

        int tileX = Mathf.FloorToInt(viewportPoint.x * width);
        int tileY = Mathf.FloorToInt(viewportPoint.y * height);

        Vector3 nearestTile = nearestTiles[tileX, tileY];
        return nearestTile + transform.position;
    }

    public void StartRoom()
    {
        RoomPlan.StartPlan();

        Globals.CurrentLevel++;
    }
}
