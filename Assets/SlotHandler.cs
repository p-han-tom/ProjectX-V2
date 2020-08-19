using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHandler : MonoBehaviour
{
    Item item;
    Image slotImage;
    Sprite itemSprite;
    void Start()
    {
        slotImage = transform.GetChild(0).GetComponent<Image>();
        if (item != null)
        {
            itemSprite = item.GetItemData().sprite;
            SetItemSprite(itemSprite);
        }
        else
        {
            SetItemSprite(null);
        }
    }
    public void SetItem(Item item)
    {
        this.item = item;
        SetItemSprite(item.GetItemData().sprite);
    }
    void SetItemSprite(Sprite sprite)
    {
        if (sprite == null) slotImage.enabled = false;
        else
        {
            slotImage.sprite = sprite;
            slotImage.enabled = true;
        }
    }
}
