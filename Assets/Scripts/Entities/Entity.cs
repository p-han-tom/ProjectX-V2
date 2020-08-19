using System.Collections;
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

    // Stats
    protected Stat maxHP;
    protected float currentHP;

    // Abilities
    protected GameObject[] weapons = new GameObject[4];
    public Vector2 castDirection;
    public Vector2 castPosition;

    protected virtual void Start()
    {
        animator = transform.Find("Sprites").Find("Sprite").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }
}
