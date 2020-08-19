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
        maxChargeDuration = .75f;
    }

    public override void Cast(Transform source, int abilityLevel) {
        base.Cast(source, abilityLevel);
        isCharging = true;
    }

    protected override void InstantiatePrefab() {
        arrow = Instantiate(arrowPrefab, source.transform.position, Quaternion.identity);
        arrow.transform.up = source.castDirection;

        arrow.GetComponent<AbilityPrefab>().source = source;
        arrow.GetComponent<ArrowProjectile>().direction = source.castDirection;
        arrow.GetComponent<ArrowProjectile>().speed = 20;
    }

    protected override void Update() {
        base.Update();
        Charge();
        
    }
}
