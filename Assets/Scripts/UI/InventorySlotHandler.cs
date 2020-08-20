using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotHandler : SlotHandler, IPointerClickHandler
{
    MouseSlotHandler mouseSlot;
    protected override void Start()
    {
        base.Start();
        mouseSlot = transform.parent.parent.parent.Find("Mouse Slot").GetComponent<MouseSlotHandler>();
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
            mouseSlot.homeSlot = this;
            RemoveItem();
        }
        else {
            SetItemData(mouseSlot.GetItemData());
            mouseSlot.RemoveItem();
        }
    }
}
