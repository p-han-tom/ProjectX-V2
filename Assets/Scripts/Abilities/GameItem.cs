﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem
{
    ItemData item;
    int abilityLevel;
    public GameItem(ItemData item, int abilityLevel) {
        this.item = item;
        this.abilityLevel = abilityLevel;
    }
    public void Cast(Vector2 direction, Vector3 mousePos, Transform source) {item.active.GetComponent<Weapon>().Cast(direction, mousePos, source, abilityLevel);}
    public ItemData GetItemData() {return item;}
    public Weapon GetActive() {return item.active.GetComponent<Weapon>();}
    public int GetAbilityLevel() {return abilityLevel;}
}
