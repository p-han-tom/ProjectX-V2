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
    KeyCode[] keyBindings = {KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.LeftShift, KeyCode.R};
    // Children
    Transform pivot;

    // Abilities
    bool abilityTriggered;
    int triggeredAbility;

    protected override void CustomStart()
    {
        movementSpeed = new Stat(5f);
        pivot = transform.Find("Pivot");
    }

    protected override void CustomUpdate()
    {
        CheckInput();
        RotateWeapon();
        if (abilityTriggered) {
            if ((int)abilityList[triggeredAbility].abilityType == 0) {
                StartCoroutine(Cast(abilityList[triggeredAbility].castTime, triggeredAbility));
                abilityTriggered = false;
            } else if ((int)abilityList[triggeredAbility].abilityType == 1) {
                Charge(keyBindings[triggeredAbility], triggeredAbility);
            } else if ((int)abilityList[triggeredAbility].abilityType == 2) {

            }
        }
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
            abilityTriggered = true;
            triggeredAbility = 0;
        }
        if (Input.GetMouseButtonDown(1))
        {
            abilityTriggered = true;
            triggeredAbility = 1;
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

    IEnumerator Cast(float castTime, int index) {
        yield return new WaitForSeconds(castTime);
        abilityList[index].Cast(direction, mousePos, transform, activeItems[index].GetAbilityLevel());
        abilityTriggered = false;
    }

    void Charge(KeyCode key, int index) {
        if (Input.GetKeyUp(key)) {
            abilityList[index].Cast(direction, mousePos, transform, activeItems[index].GetAbilityLevel());
            abilityTriggered = false;
        }
    }

    void Channel(float castTime, GameItem item) {

    }
}
