using System.Collections;
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
    public void Cast(Vector2 direction, Vector3 mousePos, Transform source) {item.ability.Cast(direction, mousePos, source, abilityLevel);}
    public ItemData GetItemData() {return item;}
    public Ability GetAbility() {return item.ability;}
    public int GetAbilityLevel() {return abilityLevel;}
}
