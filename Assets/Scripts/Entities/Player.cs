using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Mouse
    public Vector3 mousePos;
    public Vector2 direction;
    List<Item> nearbyItems = new List<Item>();

    GameObject weapon;

    // Children
    Transform pivot;

    protected override void CustomStart()
    {
        movementSpeed = new Stat(5f);
        pivot = transform.Find("Pivot");
    }

    protected override void CustomUpdate()
    {
        CheckInput();
        RotateWeapon();

    }

    void FixedUpdate()
    {
        Move();
    }

    void CheckInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }

        if (Input.GetKey(KeyCode.Mouse0)) {
            weapon.GetComponent<Weapon>().Cast(direction, mousePos, transform, 1);
        }
    }
    void PickupItem()
    {
        if (nearbyItems.Count > 0) {
            Item pickingUpItem = nearbyItems[0];
            // for (int i = 0; i < 4; i ++) {
            //     if (activeItems.items[i] == null) {
            //         activeItems.items[i] = new GameItem(pickingUpItem.GetItemData(), pickingUpItem.GetItemLevel());
            //         break;
            //     }
            // }
            weapon = Instantiate(pickingUpItem.item.active);
            

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

        pivot.localScale = (mousePos.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        pivot.up = direction;
        

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Item>() != null) {
            nearbyItems.Add(other.GetComponent<Item>());
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Item>() != null) {
            nearbyItems.Remove(other.GetComponent<Item>());
        }
    }
}
