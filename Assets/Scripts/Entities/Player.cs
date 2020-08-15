using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Mouse
    Vector3 mousePos;
    Vector2 direction;
    List<Item> nearbyItems = new List<Item>();
    GameItem[] activeItems = new GameItem[4];
    // Children
    Transform pivot;

    protected override void CustomStart()
    {
        movementSpeed = 5f;
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

        if (Input.GetMouseButtonDown(0))
        {
            abilityList[0].Cast(direction, mousePos, transform, activeItems[0].GetAbilityLevel());
        }
        if (Input.GetMouseButtonDown(1))
        {
            abilityList[1].Cast(direction, mousePos, transform, activeItems[1].GetAbilityLevel());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }
    void PickupItem()
    {
        if (nearbyItems.Count > 0) {
            Item pickingUpItem = nearbyItems[0];
            for (int i = 0; i < 4; i ++) {
                if (activeItems[i] == null) {
                    activeItems[i] = new GameItem(pickingUpItem.GetItemData(), pickingUpItem.GetItemLevel());
                    abilityList[i] = activeItems[i].GetAbility();
                    break;
                }
            }
            

            GameObject destroyThis = nearbyItems[0].gameObject;
            nearbyItems.Remove(nearbyItems[0]);
            Destroy(destroyThis);
        }
    }
    void Move()
    {
        rb.velocity = movement * movementSpeed;

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

    IEnumerator Cast(float castTime, GameItem item) {
        yield return new WaitForSeconds(castTime);
        item.GetAbility().Cast(direction, mousePos, transform, item.GetAbilityLevel());
    }

    void Charge(KeyCode key, GameItem item) {
        if (Input.GetKeyUp(key)) {
            item.GetAbility().Cast(direction, mousePos, transform, item.GetAbilityLevel());
        }
    }

    void Channel(float castTime, GameItem item) {

    }
}
