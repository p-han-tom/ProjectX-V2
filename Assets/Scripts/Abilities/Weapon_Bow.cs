using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bow : Weapon
{
    [Header("Arrow prefab")]
    public GameObject arrowPrefab;
    GameObject arrow;

    protected override string name {get {return "Bow";}}
    protected override float cooldown {get {return 0f;}}

    protected override void Start() {
        base.Start();
    }

    public override void Cast(Vector2 direction, Vector3 castPosition, Transform source, int abilityLevel) {
        base.Cast(direction, castPosition, source, abilityLevel);
        isCharging = true;
    }

    void Update() {
        
        if (triggered) {
            chargeTimer += Time.deltaTime;
        } else if (!triggered && isCharging) {
            arrow = Instantiate(arrowPrefab, source.position, Quaternion.identity);
            arrow.GetComponent<ArrowProjectile>().direction = source.GetComponent<Player>().direction;
            arrow.GetComponent<ArrowProjectile>().transform.up = source.GetComponent<Player>().direction;
            arrow.GetComponent<ArrowProjectile>().sourceLayer = source.gameObject.layer;
            arrow.GetComponent<ArrowProjectile>().speed = 20;
            isCharging = false;
        }

        triggered = false;
        
    }
}
