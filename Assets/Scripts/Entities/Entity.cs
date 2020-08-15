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
    protected float movementSpeed;

    // Stats
    protected float maxHP;
    protected float currentHP;

    // Abilities
    [SerializeField]
    protected Ability[] abilityList;

    protected virtual void CustomStart() {}
    protected virtual void CustomUpdate() {}

    void Start()
    {
        animator = transform.Find("Sprites").Find("Sprite").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        CustomStart();
    }

    void Update()
    {
        CustomUpdate();
    }
}
