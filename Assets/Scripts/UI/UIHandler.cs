﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    GameObject inventory;
    void Start()
    {
        inventory = transform.Find("Inventory").gameObject;
        CloseInventory();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory.activeSelf == false)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }
    void OpenInventory() { inventory.SetActive(true); }
    void CloseInventory() { inventory.SetActive(false); }
}