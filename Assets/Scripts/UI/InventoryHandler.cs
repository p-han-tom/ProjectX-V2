using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [HideInInspector] public InventorySlotHandler[] abilitySlots; 
    [HideInInspector] public InventorySlotHandler[] trinketSlots;
    [HideInInspector] public InventorySlotHandler [] storageSlots;
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        abilitySlots = transform.Find("Abilities").Find("Slots").GetComponentsInChildren<InventorySlotHandler>();
        trinketSlots = transform.Find("Trinkets").Find("Slots").GetComponentsInChildren<InventorySlotHandler>();
        trinketSlots = transform.Find("Storage").Find("Slots").GetComponentsInChildren<InventorySlotHandler>();
    }
    public void SetItemAbility(ItemData itemData, GameObject itemObject, int slot) {
        abilitySlots[slot].SetItemData(itemData);
        abilitySlots[slot].SetItemObject(itemObject);
    }
    public void SetItemTrinket(ItemData itemData, GameObject itemObject, int slot) {
        trinketSlots[slot].SetItemData(itemData);
        trinketSlots[slot].SetItemObject(itemObject);
    }
    public void UpdatePlayerInventory() {
        player.UpdateAbilities();
    }
}
