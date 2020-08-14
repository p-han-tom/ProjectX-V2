using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Mouse
    Vector3 mousePos;
    Vector2 direction;

    // Children
    Transform pivot;

    protected override void CustomStart() {
        movementSpeed = 5f;
        pivot = transform.Find("Pivot");
    }

    protected override void CustomUpdate()
    {
        CheckInput();
        RotateWeapon();

        if (Input.GetMouseButtonDown(0)) {
            abilityList[0].Cast(direction, mousePos, transform);
        } 
        if (Input.GetMouseButtonDown(1)) {
            abilityList[1].Cast(direction, mousePos, transform);
        }
    }

    void FixedUpdate() {
        Move();
    }

    void CheckInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2 (mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;
    }

    void Move() {
        rb.velocity = movement * movementSpeed;

        if (rb.velocity != Vector2.zero) {
            transform.rotation = (movement.x < 0) ? Quaternion.Euler(0,180,0) : Quaternion.Euler(0,0,0);
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    void RotateWeapon() {
        pivot.localScale = (mousePos.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        pivot.up = direction;
    }

    
}
