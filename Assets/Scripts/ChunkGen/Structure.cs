using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Structure", menuName = "Structure")]
public class Structure : ScriptableObject
{
    public GameObject structure;
    [Range(1,100)]
    public int spawnRate;
    [Range(1,100)]
    public int spawnsPerChunk;

}
