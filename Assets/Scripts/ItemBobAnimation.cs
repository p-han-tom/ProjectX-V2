using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobAnimation : MonoBehaviour
{
    float amplitude = 0.005f;
    float frequency = 0.5f;
    GameObject itemSprite;
    Vector3 tempPos = new Vector3();

    void Start() {
        itemSprite = transform.Find("Item Sprite").gameObject;
    }
    void Update()
    {
        tempPos = itemSprite.transform.position;
        tempPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;
        itemSprite.transform.position = tempPos;
    }
}
