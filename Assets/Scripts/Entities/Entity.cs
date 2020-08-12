using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Components
    protected Rigidbody2D rb;

    // Movement
    protected Vector2 movement;
    protected float movementSpeed;

    // Stats
    protected float maxHP;
    protected float currentHP;

    protected virtual void CustomStart() {}
    protected virtual void CustomUpdate() {}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CustomStart();
    }

    void Update()
    {
        CustomUpdate();
    }
}
