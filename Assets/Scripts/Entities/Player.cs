using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    List<Item> nearbyItems = new List<Item>();
    GameObject[] weapons = new GameObject[4];

    // Children
    Transform pivot;

    protected override void Start()
    {
        base.Start();
        movementSpeed = new Stat(5f);
        pivot = transform.Find("Pivot");
    }

    protected override void Update()
    {
        base.Update();
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
        castPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        castDirection = new Vector2(castPosition.x - transform.position.x, castPosition.y - transform.position.y).normalized;

        if (Input.GetKeyDown(KeyCode.E)) {
            PickupItem();
        }

        if (Input.GetKey(KeyCode.Mouse0)) {
            weapons[0].GetComponent<Weapon>().Cast(transform, 1);
        }

        if (Input.GetKey(KeyCode.Mouse1)) {
            weapons[1].GetComponent<Weapon>().Cast(transform, 1);
        }
    }
    void PickupItem()
    {
        if (nearbyItems.Count > 0) {
            Item pickingUpItem = nearbyItems[0];
            for (int i = 0; i < weapons.Length; i ++) {
                if (weapons[i] == null) {
                    weapons[i] = Instantiate(pickingUpItem.item.active);
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

        pivot.localScale = (castPosition.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        pivot.up = castDirection;
        

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
