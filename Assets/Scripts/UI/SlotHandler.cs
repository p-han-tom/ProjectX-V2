using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlotHandler : MonoBehaviour
{
    public enum SlotType {Active, Trinket, Storage}
    public SlotType slotType;
    [HideInInspector] public InventorySlotHandler homeSlot;
    protected GameObject itemObject;
    protected ItemData itemData;
    protected Image slotImage;
    protected virtual void Start()
    {
        slotImage = transform.GetChild(0).GetComponent<Image>();
        if (itemData != null)
        {
            SetItemSprite(itemData.sprite);
        }
        else
        {
            SetItemSprite(null);
        }
    }
    public ItemData GetItemData() {return itemData;}
    public virtual void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        if (itemData != null) SetItemSprite(itemData.sprite);
    }
    public virtual void SetItemObject(GameObject itemObject) {
        this.itemObject = itemObject;
        if (slotType == SlotType.Trinket) itemObject.GetComponent<Passive>().OnEquip();
    }
    public GameObject GetItemObject() {return itemObject;}
    public void RemoveItem() {
        if (itemData != null && slotType == SlotType.Trinket) itemObject.GetComponent<Passive>().OnUnequip();
        itemData = null;
        itemObject = null;
        slotImage.enabled = false;
    }
    protected void SetItemSprite(Sprite sprite)
    {
        if (sprite == null) slotImage.enabled = false;
        else
        {
            slotImage.sprite = sprite;
            slotImage.enabled = true;
        }
    }
}
