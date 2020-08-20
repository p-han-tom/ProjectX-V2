using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotHandler : SlotHandler, IPointerClickHandler
{
    
    InventoryHandler inventory;
    MouseSlotHandler mouseSlot;
    protected override void Start()
    {
        base.Start();
        inventory = transform.parent.parent.parent.GetComponent<InventoryHandler>();
        mouseSlot = inventory.transform.Find("Mouse Slot").GetComponent<MouseSlotHandler>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SlotClicked();
    }
    void SlotClicked()
    {
        if (mouseSlot.GetItemData() == null)
        {
            mouseSlot.SetItemData(itemData);
            mouseSlot.SetItemObject(itemObject);
            mouseSlot.homeSlot = this;
            RemoveItem();
        }
        else {
            SetItemData(mouseSlot.GetItemData());
            SetItemObject(mouseSlot.GetItemObject());
            mouseSlot.RemoveItem();
        }
        inventory.UpdatePlayerInventory();
    }
}
