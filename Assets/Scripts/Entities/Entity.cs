﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Components
    protected Rigidbody2D rb;
    protected Animator animator;

    // Movement
    protected Vector2 movement;
    protected Stat movementSpeed;

    // States
    public bool underAttack;

    // Stats
    protected Stat maxHP;
    protected float currentHP;

    // Abilities
    protected GameObject[] weapons;
    public Vector2 castDirection;
    public Vector2 castPosition;
    protected GameObject[] trinkets;

    protected virtual void Start()
    {
        animator = transform.Find("Sprites").Find("Sprite").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public void TakeDamage(float damage) {
        currentHP -= damage;
    }

    public void Knockback(Vector2 forceVector) {
        rb.AddForce(forceVector, ForceMode2D.Impulse);
    }
}
