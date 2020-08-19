using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // General
    protected abstract new string name {get;}
    protected bool triggered;
    protected Transform source;

    // CD
    protected abstract float cooldown {get;}
    protected float currentCooldown;

    // Sprite stuff
    [Header("Sprite values")]
    public float spriteRotation;
    public Sprite sprite;
    public Sprite[] chargeSprites;

    // Components
    protected Animator animator;
    protected Rigidbody2D rb;

    // Charge 
    [HideInInspector] public KeyCode chargeKey;
    protected float chargeTimer;
    protected float maxChargeDuration;
    protected float chargeStrength = 1;
    protected bool isCharging;


    protected virtual void Start(){
        Debug.Log("hello?");
        rb = GetComponent<Rigidbody2D>();
    }


    /* Casting methods */
    public virtual void Cast(Vector2 direction, Vector3 castPosition, Transform source, int abilityLevel) {
        EquipSprite(source);
        triggered = true;
        this.source = source;
    }

    /* *************** */


    protected virtual void EquipSprite(Transform source) {
        source.Find("Pivot").Find("Weapon Pivot").Find("Held Weapon").GetComponent<SpriteRenderer>().sprite = sprite;
        source.Find("Pivot").Find("Weapon Pivot").Find("Held Weapon").transform.localEulerAngles = new Vector3(0,0,spriteRotation);
    }
    public virtual void StartCooldown() {currentCooldown = cooldown;}
    public virtual void DecreaseCooldown() {currentCooldown -= Time.deltaTime;}
    public bool OnCooldown() {return (currentCooldown > 0) ? true : false;}
    public float GetRemainingCooldown() {return currentCooldown;}
    public float GetCooldown() {return cooldown;}
    
}


