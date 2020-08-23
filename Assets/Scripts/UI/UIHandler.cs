using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    GameObject inventory;
    GameObject abilityBar;
    public bool isInventoryOpen = false;
    Vector3 abilityBarStartPos;
    Vector3 inventoryStartPos;
    MouseSlotHandler mouseSlot;
    Image darkener;
    public AbilitySlotHandler[] abilitySlots;
    Player player;
    Ease transitionEase = Ease.InOutCubic;
    float transitionSpeed = 0.3f;
    
    void Start()
    {
        inventory = transform.Find("Inventory").gameObject;
        abilityBar = transform.Find("Ability Bar").gameObject;
        darkener = transform.Find("Darkener").GetComponent<Image>();
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
        darkener.DOFade(0.3f, transitionSpeed);
        abilityBar.transform.DOLocalMoveY(-300, transitionSpeed).SetEase(transitionEase);
        inventory.transform.DOLocalMoveY(0, transitionSpeed).SetEase(transitionEase);
    }
    void CloseInventory()
    {
        darkener.DOFade(0, transitionSpeed);
        mouseSlot.ReturnItemToHome();
        abilityBar.transform.DOLocalMoveY(abilityBarStartPos.y, transitionSpeed).SetEase(transitionEase);
        inventory.transform.DOLocalMoveY(inventoryStartPos.y, transitionSpeed).SetEase(transitionEase);
    }
}
