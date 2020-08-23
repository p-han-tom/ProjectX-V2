using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    // roomsources[0] is always walls.png (all black)
    public Sprite[] roomSources;
    public Tile wallTiles;
    public Tile groundTiles;
    int roomWidth = 20;
    int roomHeight = 16;
    enum Direction { up, down, left, right }
    int[,] dungeonIntArray = new int[4, 4];
    int numberOfRooms = 6;
    Tilemap wallTilemap;
    Tilemap groundTilemap;
    void Start()
    {
        groundTilemap = GameObject.Find("Grid").transform.Find("Ground").GetComponent<Tilemap>();
        wallTilemap = GameObject.Find("Grid").transform.Find("Walls").GetComponent<Tilemap>();
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
            }
        }
    }
    void AddRooms()
    {
        // Start room
        int randomx = Random.Range(0, dungeonIntArray.GetLength(0) - 1);
        int randomy = Random.Range(0, dungeonIntArray.GetLength(1) - 1);
        dungeonIntArray[randomx, randomy] = RandomRoomIndex();
        GameObject.Find("Player").transform.position = new Vector3(randomy*roomWidth + roomWidth/2, randomx*roomHeight + roomHeight/2, 1);
        Camera.main.GetComponent<CameraControl>().TeleportToLeader();
        // todo: generation logic :|
    }
    void DrawTiles()
    {
        for (int i = 0; i < dungeonIntArray.GetLength(0); i++)
        {
            for (int j = 0; j < dungeonIntArray.GetLength(1); j++)
            {
                Texture2D roomSource = roomSources[dungeonIntArray[i, j]].texture;
                for (int k = 0; k < roomHeight; k++)
                {
                    for (int l = 0; l < roomWidth; l++)
                    {
                        Vector3Int tilePosition = new Vector3Int(j * roomWidth + l, i * roomHeight + k, 1);
                        if (roomSource.GetPixel(l, k) == Color.black)
                        {
                            wallTilemap.SetTile(tilePosition, wallTiles);
                        }
                        else {
                            groundTilemap.SetTile(tilePosition, groundTiles);
                        }
                    }
                }
            }
        }
    }
    int RandomRoomIndex() {
        return Random.Range(1, roomSources.Length);
    }
    Direction RandomDirection() {
        return (Direction)Random.Range(0,4);
    }
}
