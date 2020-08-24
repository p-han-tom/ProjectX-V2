using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public Sprite filledRoomSource;
    public Sprite[] specialRoomSources;
    public Sprite[] roomSources;
    public RuleTile wallTiles;
    public Tile groundTiles;
    int roomWidth = 20;
    int roomHeight = 16;
    enum Direction { up, down, left, right }
    enum RoomType { normal, special, filled } // maybe prefab room type for special rooms
    int[,] dungeonIntArray = new int[10, 10];
    RoomType[,] dungeonRoomTypeArray;
    int numberOfRooms = 6;
    int numberOfWalkers = 2;
    int walkerLifespan = 4;
    Tilemap wallTilemap;
    Tilemap groundTilemap;
    void Start()
    {
        groundTilemap = GameObject.Find("Grid").transform.Find("Ground").GetComponent<Tilemap>();
        wallTilemap = GameObject.Find("Grid").transform.Find("Walls").GetComponent<Tilemap>();
        dungeonRoomTypeArray = new RoomType[dungeonIntArray.GetLength(0), dungeonIntArray.GetLength(1)];
        FillWithWalls();
        AddRooms();
        DrawTiles();
    }
    void FillWithWalls()
    {
        for (int i = 0; i < dungeonIntArray.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonIntArray.GetLength(1); j++)
            {
                dungeonIntArray[i, j] = 0;
                dungeonRoomTypeArray[i, j] = RoomType.filled;
            }
        }
    }
    void AddRooms()
    {
        // Start room
        // int randomx = Random.Range(0, dungeonIntArray.GetLength(0) - 1);
        // int randomy = Random.Range(0, dungeonIntArray.GetLength(1) - 1);
        int randomx = dungeonIntArray.GetLength(1) / 2;
        int randomy = dungeonIntArray.GetLength(0) / 2;
        // dungeonIntArray[randomx, randomy] = RandomRoomIndex();
        dungeonIntArray[randomx, randomy] = 0; // 0 is index of starting room
        dungeonRoomTypeArray[randomx, randomy] = RoomType.special;
        GameObject.Find("Player").transform.position = new Vector3(randomy * roomWidth + roomWidth / 2, randomx * roomHeight + roomHeight / 2, 1);
        Camera.main.GetComponent<CameraControl>().TeleportToLeader();
        for (int w = 0; w < numberOfWalkers; w++)
        {
            Vector3Int walkerPos = new Vector3Int(randomx, randomy, 1);
            for (int r = 0; r < walkerLifespan; r++)
            {
                Direction dir = RandomDirection();
                switch (dir)
                {
                    case Direction.up:
                        {
                            walkerPos += new Vector3Int(0, 1, 0);
                            break;
                        }
                    case Direction.down:
                        {
                            walkerPos += new Vector3Int(0, -1, 0);
                            break;
                        }
                    case Direction.left:
                        {
                            walkerPos += new Vector3Int(-1, 0, 0);
                            break;
                        }
                    case Direction.right:
                        {
                            walkerPos += new Vector3Int(1, 0, 0);
                            break;
                        }
                }
                if (IsValidSpace(walkerPos.x, walkerPos.y) && dungeonRoomTypeArray[walkerPos.y, walkerPos.x] != RoomType.special)
                {
                    dungeonIntArray[walkerPos.y, walkerPos.x] = RandomRoomIndex();
                    dungeonRoomTypeArray[walkerPos.y, walkerPos.x] = RoomType.normal;
                }
                else break;
            }
        }
    }
    void DrawTiles()
    {
        for (int i = 0; i < dungeonIntArray.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonIntArray.GetLength(1); j++)
            {
                if (dungeonRoomTypeArray[i, j] != RoomType.filled)
                {
                    Texture2D roomSource = null;
                    if (dungeonRoomTypeArray[i, j] == RoomType.normal)
                    {
                        roomSource = roomSources[dungeonIntArray[i, j]].texture;
                    }
                    else if (dungeonRoomTypeArray[i, j] == RoomType.special)
                    {
                        roomSource = specialRoomSources[dungeonIntArray[i, j]].texture;
                    }
                    for (int k = 0; k < roomHeight; k++)
                    {
                        for (int l = 0; l < roomWidth; l++)
                        {
                            Vector3Int tilePosition = new Vector3Int(j * roomWidth + l, i * roomHeight + k, 1);
                            Color pixelColor = roomSource.GetPixel(l, k);
                            if (pixelColor == Color.black)
                            {
                                wallTilemap.SetTile(tilePosition, wallTiles);
                            }
                            else if (pixelColor == Color.white)
                            {
                                groundTilemap.SetTile(tilePosition, groundTiles);
                            }
                            // else if red, spawn enemy and put ground tile
                        }
                    }
                }
            }
        }
        for (int i = 0; i < dungeonIntArray.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonIntArray.GetLength(1); j++)
            {
                // create exits between adjacent rooms
                if (dungeonRoomTypeArray[i, j] != RoomType.filled)
                {
                    // up
                    if (IsValidSpace(i - 1, j) && dungeonRoomTypeArray[i - 1, j] != RoomType.filled)
                    {
                        Vector3Int pos1 = new Vector3Int(j * roomWidth + roomWidth / 2, i * roomHeight - 1, 1);
                        ConnectRoom(pos1, new Vector3Int(-1, 0, 0));
                    }
                    //down
                    if (IsValidSpace(i + 1, j) && dungeonRoomTypeArray[i + 1, j] != RoomType.filled)
                    {
                        Vector3Int pos1 = new Vector3Int(j * roomWidth + roomWidth / 2, (i + 1) * roomHeight, 1);
                        ConnectRoom(pos1, new Vector3Int(-1, 0, 0));
                    }
                    // left
                    if (IsValidSpace(i, j - 1) && dungeonRoomTypeArray[i, j - 1] != RoomType.filled)
                    {
                        Vector3Int pos1 = new Vector3Int(j * roomWidth, i * roomHeight + roomHeight / 2, 1);
                        ConnectRoom(pos1, new Vector3Int(0, -1, 0));
                    }
                    // right
                    if (IsValidSpace(i, j + 1) && dungeonRoomTypeArray[i, j + 1] != RoomType.filled)
                    {
                        Vector3Int pos1 = new Vector3Int((j + 1) * roomWidth - 1, i * roomHeight + roomHeight / 2, 1);
                        ConnectRoom(pos1, new Vector3Int(0, -1, 0));
                    }
                }
            }
        }
    }
    int RandomRoomIndex() { return Random.Range(3, roomSources.Length); }
    Direction RandomDirection() { return (Direction)Random.Range(0, 4); }
    bool IsValidSpace(int col, int row)
    {
        if (col < dungeonIntArray.GetLength(0) && col >= 0 && row < dungeonIntArray.GetLength(1) && row >= 0)
        {
            return true;
        }
        return false;
    }
    void ConnectRoom(Vector3Int pos1, Vector3Int add)
    {
        Vector3Int pos2 = pos1 + add;
        wallTilemap.SetTile(pos1, null);
        wallTilemap.SetTile(pos2, null);
        groundTilemap.SetTile(pos1, groundTiles);
        groundTilemap.SetTile(pos2, groundTiles);
    }
}
