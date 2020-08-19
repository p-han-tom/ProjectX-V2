using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected abstract new string name {get;}

    protected abstract float cooldown {get;}
    protected float currentCooldown;

    // Components
    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void CustomStart() {}
    public abstract void Cast(Transform source, int abilityLevel);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CustomStart();
    }

    public virtual void StartCooldown() {currentCooldown = cooldown;}
    public virtual void DecreaseCooldown() {currentCooldown -= Time.deltaTime;}
    public bool OnCooldown() {return (currentCooldown > 0) ? true : false;}
    public float GetRemainingCooldown() {return currentCooldown;}
    public float GetCooldown() {return cooldown;}


    
}
