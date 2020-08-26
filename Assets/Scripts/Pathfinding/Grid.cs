﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGridGizmos;

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;


    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
        return grid[x,y];
    }

    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x ++) {
            for (int y = -1; y <= 1; y ++) {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);

                    if (!grid[checkX, checkY].walkable) {
                        if (x != 0 && y != 0)
                            if (!grid[checkX - x, checkY].walkable || !grid[checkX, checkY-y].walkable)
                                continue;
                        else if (x == 0 || y == 0)
                            if (grid[node.gridX - x, node.gridY - y].walkable) continue;

                        neighbours.Clear();
                        if (node.gridX - 1 >= 0) neighbours.Add(grid[node.gridX-1, node.gridY]);
                        if (node.gridX + 1 < gridSizeX) neighbours.Add(grid[node.gridX+1, node.gridY]);
                        if (node.gridY - 1 >= 0) neighbours.Add(grid[node.gridX, node.gridY-1]);
                        if (node.gridY + 1 < gridSizeY) neighbours.Add(grid[node.gridX, node.gridY+1]);
                        return neighbours;
                    }
                }
            }
        }

        return neighbours;
    }

    void Awake() {
        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

        CreateGrid();
    }


    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));
        if (grid != null && displayGridGizmos) {
            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? new Color(1,1,1,0.25f) : Color.red;
                Gizmos.DrawCube(n.worldPosition, new Vector3(1,1,0) * (nodeDiameter-0.4f));
            }
        }
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;

        for (int x = 0; x < gridSizeX; x ++) {
            for (int y = 0; y < gridSizeY; y ++) {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius/2, unwalkableMask));
                
                grid[x,y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
}
