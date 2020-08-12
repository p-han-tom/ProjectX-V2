using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Movement
    Vector2 movement;
    float movementSpeed = 5f;

    // Components
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void CheckInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        rb.velocity = movement * movementSpeed;
    }

    void Update()
    {
        CheckInput();
        Move();
    }
}
