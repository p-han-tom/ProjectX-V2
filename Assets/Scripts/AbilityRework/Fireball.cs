using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    public GameObject fireballProjectile;
    public override float GetCooldown() => 5f;
    public override float GetDamage() => 10f;
    
    public override void Cast(Vector2 direction, Vector3 mousePos, Transform source) {

        Instantiate(fireballProjectile, source.position, Quaternion.identity);
        fireballProjectile.GetComponent<FireballProjectile>().direction = direction;
        
    }
}
