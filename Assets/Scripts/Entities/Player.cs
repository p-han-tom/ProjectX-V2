using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    List<Item> nearbyItems = new List<Item>();

    // Children
    Transform pivot;
    UIHandler ui;
    InventoryHandler inventory;
    HUDHandler hud;

    protected override void Start()
    {
        base.Start();
        movementSpeed = new Stat(5f);
        pivot = transform.Find("Pivot");
        weapons = new GameObject[4];
        trinkets = new GameObject[4];
        ui = GameObject.Find("UI").GetComponent<UIHandler>();
        inventory = ui.transform.Find("Inventory").GetComponent<InventoryHandler>();
        hud = ui.transform.Find("HUD").GetComponent<HUDHandler>();
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
        RotateWeapon();
        UpdatePassives();
    }

    void FixedUpdate()
    {
        if (underAttack) return;
        else Move();
    }

    void CheckInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        castPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        castDirection = new Vector2(castPosition.x - transform.position.x, castPosition.y - transform.position.y).normalized;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            weapons[0].GetComponent<Active>().Cast(transform, 1);

        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            weapons[1].GetComponent<Active>().Cast(transform, 1);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            weapons[2].GetComponent<Active>().Cast(transform, 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            weapons[3].GetComponent<Active>().Cast(transform, 1);
        }
    }
    void PickupItem()
    {
        if (nearbyItems.Count > 0)
        {
            Item pickingUpItem = nearbyItems[0];
            GameObject effect = Instantiate(pickingUpItem.item.active);
            if (pickingUpItem.GetItemData().itemType == ItemData.ItemType.Active)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i] == null)
                    {
                        inventory.SetItemAbility(pickingUpItem.GetItemData(), effect, i);
                        weapons[i] = effect;
                        weapons[i].transform.parent = transform;
                        break;
                    }
                }
            }
            else if (pickingUpItem.GetItemData().itemType == ItemData.ItemType.Trinket)
            {
                for (int i = 0; i < trinkets.Length; i++)
                {
                    if (trinkets[i] == null)
                    {
                        effect.GetComponent<Passive>().owner = this;
                        inventory.SetItemTrinket(pickingUpItem.GetItemData(), effect, i);
                        trinkets[i] = effect;
                        trinkets[i].transform.parent = transform;
                        break;
                    }
                }
            }
            GameObject destroyThis = nearbyItems[0].gameObject;
            nearbyItems.Remove(nearbyItems[0]);
            Destroy(destroyThis);
            UpdateAbilities();
        }
    }
    public void UpdateAbilities()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = inventory.abilitySlots[i].GetItemObject();
            hud.abilitySlots[i].RemoveItem();
            hud.abilitySlots[i].SetItemData(inventory.abilitySlots[i].GetItemData());
            hud.abilitySlots[i].SetItemObject(inventory.abilitySlots[i].GetItemObject());
        }
        for (int i = 0; i < trinkets.Length; i++)
        {
            trinkets[i] = inventory.trinketSlots[i].GetItemObject();
        }
    }
    void Move()
    {
        rb.velocity = movement * movementSpeed.value();

        if (rb.velocity != Vector2.zero)
        {
            transform.rotation = (movement.x < 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void RotateWeapon()
    {
        pivot.localScale = (castPosition.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        pivot.up = castDirection;
    }
    void UpdatePassives()
    {
        foreach (GameObject trinket in trinkets)
        {
            if (trinket != null)
            {
                Passive passiveComponent = trinket.GetComponent<Passive>();
                if (passiveComponent != null)
                {
                    trinket.GetComponent<Passive>().UpdateTimeElapsed();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter2DAddNearbyItems(other);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit2DRemoveLeftItems(other);
    }
    void OnTriggerEnter2DAddNearbyItems(Collider2D other)
    {
        if (other.GetComponent<Item>() != null)
        {
            nearbyItems.Add(other.GetComponent<Item>());
        }
    }
    void OnTriggerExit2DRemoveLeftItems(Collider2D other)
    {
        if (other.GetComponent<Item>() != null)
        {
            nearbyItems.Remove(other.GetComponent<Item>());
        }
    }
}
