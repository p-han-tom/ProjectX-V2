﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    
    public new string name;

    [Range(1,5)]
    public int rarity;
    public Sprite sprite;


}