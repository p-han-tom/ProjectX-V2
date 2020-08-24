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

    // States
    [HideInInspector] public bool underAttack;

    // Stats
    protected Stat maxHP;
    protected float currentHP;

    // Modifiers
    public Stat modHP = new Stat(1);
    public Stat modAttackSize = new Stat(1);
    public Stat modMeleeAttackSize = new Stat(1);
    public Stat modRangedAttackSize = new Stat(1);

    // Abilities
    protected GameObject[] weapons;
    [HideInInspector] public Vector2 castDirection;
    [HideInInspector] public Vector2 castPosition;
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
