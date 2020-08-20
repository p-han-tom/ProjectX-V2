using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlotHandler : MonoBehaviour
{
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
        SetItemSprite(itemData.sprite);
    }
    public void RemoveItem() {
        itemData = null;
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
