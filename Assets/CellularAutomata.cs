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
    int mapHeight = 100;
    string seed;
    int randomFillPercent = 30;
    int smoothIterations = 10;
    int buildingAttempts = 50;
    int[,] mapArray;
    int[,] mapArrayNew;
    public Sprite[] sprites;
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

        if (seed == null || seed == "")
        {
            seed = System.DateTime.Now.ToString();
            Debug.Log("Using random seed: " + seed);
        }
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
                if (j == 0 || j == 1 || j == mapWidth - 1 || j == mapWidth - 2 || i == 0 || i == mapHeight - 1 || i == 1 || i == mapHeight - 2)
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
        PlaceBuildings();
        DrawTiles();
        GameObject.Find("Player").transform.position = new Vector3(mapWidth / 2, mapHeight/2, 1);
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
    void PlaceBuildings()
    {
        for (int a = 0; a < buildingAttempts; a++)
        {
            Texture2D randomBuildingTexture = sprites[Random.Range(0, sprites.Length)].texture;
            int[,] buildingArray = new int[randomBuildingTexture.height, randomBuildingTexture.width];
            for (int i = 0; i < buildingArray.GetLength(0); i++)
            {
                for (int j = 0; j < buildingArray.GetLength(1); j++)
                {
                    buildingArray[i, j] = (randomBuildingTexture.GetPixel(j, i) == Color.black) ? 1 : 0;
                }
            }
            int rotations = Random.Range(0, 4);
            int randomx = Random.Range(0, mapWidth);
            int randomy = Random.Range(0, mapHeight);
            bool isClear = true;
            for (int checkY = 0; checkY < buildingArray.GetLength(0); checkY++)
            {
                for (int checkX = 0; checkX < buildingArray.GetLength(1); checkX++)
                {
                    if (!IsValidSpace(checkY + randomy, checkX + randomx) || mapArray[checkY + randomy, checkX + randomx] == 1)
                    {
                        isClear = false;
                        Debug.Log("NOT CLEAR!");
                        break;
                    }
                }
                if (isClear == false) break;
            }
            if (isClear == true)
            {
                for (int checkY = 0; checkY < buildingArray.GetLength(0); checkY++)
                {
                    for (int checkX = 0; checkX < buildingArray.GetLength(1); checkX++)
                    {
                        mapArray[checkY + randomy, checkX + randomx] = buildingArray[checkY, checkX];
                    }
                }
            }
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
    bool IsValidSpace(int row, int col)
    {
        if (col < mapWidth && col >= 0 && row < mapHeight && row >= 0)
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
                    if (i != col || j != row)
                    {
                        if (mapArray[i, j] == 1)
                            counter++;
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

