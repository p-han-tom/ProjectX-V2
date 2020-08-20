using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    List<Item> nearbyItems = new List<Item>();

    // Children
    Transform pivot;
    InventoryHandler inventory;

    protected override void Start()
    {
        base.Start();
        movementSpeed = new Stat(5f);
        pivot = transform.Find("Pivot");
        weapons = new GameObject[4];
        trinkets = new GameObject[4];
        inventory = GameObject.Find("UI").transform.Find("Inventory").GetComponent<InventoryHandler>();
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
            weapons[0].GetComponent<Weapon>().Cast(transform, 1);
            
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            weapons[1].GetComponent<Weapon>().Cast(transform, 1);
        }
    }
    void PickupItem()
    {
        if (nearbyItems.Count > 0)
        {
            Item pickingUpItem = nearbyItems[0];
            GameObject effect = Instantiate(pickingUpItem.item.active);
            if (effect.GetComponent<Weapon>() != null)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i] == null)
                    {
                        inventory.SetItemAbility(pickingUpItem.GetItemData(), i);
                        weapons[i] = effect;
                        weapons[i].transform.parent = transform;
                        break;
                    }
                }
            }
            else if (effect.GetComponent<Passive>() != null)
            {
                for (int i = 0; i < trinkets.Length; i++)
                {
                    if (trinkets[i] == null)
                    {
                        inventory.SetItemTrinket(pickingUpItem.GetItemData(), i);
                        trinkets[i] = effect;
                        trinkets[i].transform.parent = transform;
                        effect.GetComponent<Passive>().owner = this;
                        break;
                    }
                }
            }

            GameObject destroyThis = nearbyItems[0].gameObject;
            nearbyItems.Remove(nearbyItems[0]);
            Destroy(destroyThis);
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
        if (other.GetComponent<Item>() != null)
        {
            nearbyItems.Add(other.GetComponent<Item>());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Item>() != null)
        {
            nearbyItems.Remove(other.GetComponent<Item>());
        }
    }
}
