using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSlotHandler : SlotHandler
{
    Camera cameraMain;
    protected override void Start()
    {
        base.Start();
        cameraMain = Camera.main;
    }
    public override void SetItemData(ItemData itemData) {
        base.SetItemData(itemData);
    }
    private void Update()
    {
        if (itemData != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = cameraMain.ScreenToWorldPoint(mousePos);
            mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, mousePos, 5f);
        }
    }
    private void OnDisable() {
        ReturnItemToHome();
    }
    public void ReturnItemToHome() {
        if (itemData != null && itemObject != null) {
            homeSlot.SetItemData(itemData);
            homeSlot.SetItemObject(itemObject);
            RemoveItem();
        }
    }
}
