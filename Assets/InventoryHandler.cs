using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    SlotHandler[] abilitySlots; 
    SlotHandler[] trinketSlots;
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        abilitySlots = transform.Find("Abilities").Find("Slots").GetComponentsInChildren<SlotHandler>();
        trinketSlots = transform.Find("Trinkets").Find("Slots").GetComponentsInChildren<SlotHandler>();
    }
    public void SetItemAbility(Item item, int slot) {
        abilitySlots[slot].SetItem(item);
    }
    public void SetItemTrinket(Item item, int slot) {
        trinketSlots[slot].SetItem(item);
    }
}
