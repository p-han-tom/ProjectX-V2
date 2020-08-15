using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    
    public new string name;
    public Sprite sprite;

    [Range(1,5)]
    public int rarity;
    public Ability ability;


}
