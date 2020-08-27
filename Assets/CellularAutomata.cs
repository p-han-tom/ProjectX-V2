using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellularAutomata : MonoBehaviour
{
    public RuleTile wallTile;
    public Tile groundTile;
    Tilemap wallTilemap;
    Tilemap groundTilemap;
    int mapWidth = 80;
    int mapHeight = 50;
    string seed;
    [Range(0, 100)]
    int randomFillPercent = 35;
    int smoothIterations = 5;
    int[,] mapArray;
    int[,] mapArrayNew;
    void Start()
    {
        groundTilemap = GameObject.Find("Grid").transform.Find("Ground").GetComponent<Tilemap>();
        wallTilemap = GameObject.Find("Grid").transform.Find("Walls").GetComponent<Tilemap>();
        GenerateMap();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateMap();
        }
    }
    void GenerateMap()
    {
        mapArray = new int[mapHeight, mapWidth];
        mapArrayNew = new int[mapHeight, mapWidth];

        if (seed == null || seed == "") seed = Time.time.ToString();
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
                if (j == 0 || j == mapWidth - 1 || i == 0 || i == mapHeight - 1)
                {
                    mapArray[i, j] = 1;
                }
                else
                {
                    mapArray[i, j] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
        SmoothMap();
        DrawTiles();
        GameObject.Find("Player").transform.position = new Vector3(mapWidth / 2, mapHeight / 2, 1);
        Camera.main.GetComponent<CameraControl>().TeleportToLeader();
    }
    void SmoothMap()
    {
        mapArrayNew = mapArray;
        for (int s = 0; s < smoothIterations; s++)
        {
            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    int count = GetSurroundingWallCount(i, j);
                    if (count > 4)
                        mapArrayNew[i, j] = 1;
                    else if (count < 4)
                        mapArrayNew[i, j] = 0;
                }
            }
            mapArray = mapArrayNew;
        }
    }

    void DrawTiles()
    {
        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
                int tileInt = mapArray[i, j];
                if (tileInt == 1)
                {
                    wallTilemap.SetTile(new Vector3Int(j, i, 1), wallTile);
                }
                else if (tileInt == 0)
                {
                    groundTilemap.SetTile(new Vector3Int(j, i, 1), groundTile);
                }
            }
        }
    }
    bool IsValidSpace(int col, int row)
    {
        if (col < mapArray.GetLength(0) && col >= 0 && row < mapArray.GetLength(1) && row >= 0)
        {
            return true;
        }
        return false;
    }
    int GetSurroundingWallCount(int col, int row)
    {
        int counter = 0;
        for (int i = col - 1; i <= col + 1; i++)
        {
            for (int j = row - 1; j <= row + 1; j++)
            {
                if (IsValidSpace(i, j))
                {
                    if (i != row && j != col)
                    {
                        counter += mapArray[i, j];
                    }
                }
                else
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}
