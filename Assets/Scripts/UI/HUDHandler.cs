using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    Player player;
    InventoryHandler inventory;
    public AbilitySlotHandler[] abilitySlots;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = transform.parent.Find("Inventory").GetComponent<InventoryHandler>();
        abilitySlots = transform.Find("Ability Bar").GetComponentsInChildren<AbilitySlotHandler>();
    }

    void Update()
    {
        
    }
}
