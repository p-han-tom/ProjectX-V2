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
    public void Cast(Transform source) {item.active.GetComponent<Active>().Cast(source, abilityLevel);}
    public ItemData GetItemData() {return item;}
    public Active GetActive() {return item.active.GetComponent<Active>();}
    public int GetAbilityLevel() {return abilityLevel;}
}
