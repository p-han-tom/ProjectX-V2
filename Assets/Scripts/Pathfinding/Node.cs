using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;

    // distance from start
    public int gCost;

    // distance from end
    public int hCost;

    public int gridX, gridY;

    public Node parent;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY) {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}
