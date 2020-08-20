using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    InventorySlotHandler[] abilitySlots; 
    InventorySlotHandler[] trinketSlots;
    InventorySlotHandler [] storageSlots;
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        abilitySlots = transform.Find("Abilities").Find("Slots").GetComponentsInChildren<InventorySlotHandler>();
        trinketSlots = transform.Find("Trinkets").Find("Slots").GetComponentsInChildren<InventorySlotHandler>();
        trinketSlots = transform.Find("Storage").Find("Slots").GetComponentsInChildren<InventorySlotHandler>();
    }
    public void SetItemAbility(ItemData itemData, int slot) {
        abilitySlots[slot].SetItemData(itemData);
    }
    public void SetItemTrinket(ItemData itemData, int slot) {
        trinketSlots[slot].SetItemData(itemData);
    }
}
