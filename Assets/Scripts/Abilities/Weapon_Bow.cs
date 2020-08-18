using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bow : Weapon
{
    
    public GameObject arrowPrefab;
    GameObject arrow;

    protected override string name {get {return "Bow";}}
    protected override float cooldown {get {return 0f;}}

    public override void Cast(Vector2 direction, Vector3 castPosition, Transform source, int abilityLevel) {
        arrow = Instantiate(arrowPrefab, source.position, Quaternion.identity);
        arrow.GetComponent<ArrowProjectile>().direction = direction;
        arrow.GetComponent<ArrowProjectile>().transform.up = direction;
        arrow.GetComponent<ArrowProjectile>().sourceLayer = source.gameObject.layer;
        arrow.GetComponent<ArrowProjectile>().speed = 20;
    }


    // Update is called once per frame
    void Update()
    {
     
    }
}
