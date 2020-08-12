using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    protected override void CustomStart() {
        movementSpeed = 5f;
    }

    protected override void CustomUpdate()
    {
        CheckInput();
        Move();
    }

    void CheckInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        rb.velocity = movement * movementSpeed;
    }

    
}
