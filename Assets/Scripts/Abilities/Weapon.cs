﻿using System.Collections;
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
    protected int chargeIntervals;


    protected virtual void Start(){
        rb = GetComponent<Rigidbody2D>();
        chargeIntervals = chargeSprites.Length;

    }


    /* Casting methods */
    public virtual void Cast(Vector2 direction, Vector3 castPosition, Transform source, int abilityLevel) {
        if (!isCharging) EquipSprite(source, sprite);
        triggered = true;
        this.source = source;
    }

    protected virtual void InstantiatePrefab() {}

    /* Charging methods */
    protected virtual void UpdateChargedSprite() {
        for (int i = 0; i < chargeIntervals; i ++) {
            if (chargeTimer < (maxChargeDuration/chargeIntervals)*i) {
                EquipSprite(source, chargeSprites[i]);
                break;
            } 
        }
        
    }

    protected virtual void Charge() {
        if (triggered) {
            chargeTimer += Time.deltaTime;
            UpdateChargedSprite();
        } else if (!triggered && isCharging) {
            InstantiatePrefab();
            isCharging = false;
            chargeTimer = 0;
            EquipSprite(source, sprite);
        }

        triggered = false;
    }

    /* *************** */


    protected virtual void EquipSprite(Transform source, Sprite sprite) {
        source.Find("Pivot").Find("Weapon Pivot").Find("Held Weapon").GetComponent<SpriteRenderer>().sprite = sprite;
        source.Find("Pivot").Find("Weapon Pivot").Find("Held Weapon").transform.localEulerAngles = new Vector3(0,0,spriteRotation);
    }
    public virtual void StartCooldown() {currentCooldown = cooldown;}
    public virtual void DecreaseCooldown() {currentCooldown -= Time.deltaTime;}
    public bool OnCooldown() {return (currentCooldown > 0) ? true : false;}
    public float GetRemainingCooldown() {return currentCooldown;}
    public float GetCooldown() {return cooldown;}
    
}


