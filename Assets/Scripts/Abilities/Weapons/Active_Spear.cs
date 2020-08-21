using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Spear : Active
{
    [Header("Thrust Prefab")]
    public GameObject thrustPrefab;
    GameObject thrust;
    
    protected override string name {get {return "Spear";}}
    protected override float cooldown {get {return .5f;}}

    protected override void InstantiatePrefab() {
        thrust = Instantiate(thrustPrefab, source.transform.position, Quaternion.identity);
        thrust.GetComponent<AbilityPrefab>().source = source;
        thrust.GetComponent<AbilityPrefab>().knockbackPower = 15f;
        ModifyMeleeSize(thrust);
        thrust.transform.up = source.GetComponent<Entity>().castDirection;
        thrust.transform.position += new Vector3(source.castDirection.x, source.castDirection.y, 0) * 2f;
    }

    public override void Cast(Transform source, int abilityLevel) {
        base.Cast(source, abilityLevel);
        if (OnCooldown()) return;

        source.Find("Pivot").Find("Weapon Pivot").GetComponent<Animator>().SetTrigger("Thrust");
        InstantiatePrefab(); 
        StartCooldown();
       
    }

    protected override void Update() {
        base.Update();
    }
}
