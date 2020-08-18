using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected abstract new string name {get;}

    protected abstract float cooldown {get;}
    protected float currentCooldown;
    public KeyCode key;

    // Components
    protected Animator animator;
    protected Rigidbody2D rb;

    public abstract void Cast(Vector2 direction, Vector3 castPosition, Transform source, int abilityLevel);

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void StartCooldown() {currentCooldown = cooldown;}
    public virtual void DecreaseCooldown() {currentCooldown -= Time.deltaTime;}
    public bool OnCooldown() {return (currentCooldown > 0) ? true : false;}
    public float GetRemainingCooldown() {return currentCooldown;}
    public float GetCooldown() {return cooldown;}
    
}


