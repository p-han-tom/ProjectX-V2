using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData item;
    public int itemLevel;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = transform.Find("Sprites").Find("Item Sprite").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
    }
    public ItemData GetItemData() {return item;}
    public int GetItemLevel() {return itemLevel;}
}
