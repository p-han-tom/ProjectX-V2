using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    GameObject inventory;
    GameObject abilityBar;
    public bool isInventoryOpen = false;
    Vector3 abilityBarStartPos;
    Vector3 inventoryStartPos;
    MouseSlotHandler mouseSlot;
    public AbilitySlotHandler[] abilitySlots;
    Player player;
    
    void Start()
    {
        inventory = transform.Find("Inventory").gameObject;
        abilityBar = transform.Find("Ability Bar").gameObject;
        inventoryStartPos = inventory.transform.localPosition;
        abilityBarStartPos = abilityBar.transform.localPosition;
        player = GameObject.Find("Player").GetComponent<Player>();
        abilitySlots = transform.Find("Ability Bar").GetComponentsInChildren<AbilitySlotHandler>();
        mouseSlot = inventory.transform.Find("Mouse Slot").GetComponent<MouseSlotHandler>();
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
            if (isInventoryOpen == false)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
            isInventoryOpen = !isInventoryOpen;
        }
    }
    void OpenInventory()
    {
        abilityBar.transform.DOLocalMoveY(-300, 0.5f);
        inventory.transform.DOLocalMoveY(0, 0.5f);
    }
    void CloseInventory()
    {
        mouseSlot.ReturnItemToHome();
        abilityBar.transform.DOLocalMoveY(abilityBarStartPos.y, 0.5f);
        inventory.transform.DOLocalMoveY(inventoryStartPos.y, 0.5f);
    }
}
