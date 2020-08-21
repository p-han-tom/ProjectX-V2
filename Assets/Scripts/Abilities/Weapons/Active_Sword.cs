using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Sword : Active
{
    [Header("Crescent Prefab")]
    public GameObject crescentPrefab;
    GameObject crescent;
    
    protected override string name {get {return "Sword";}}
    protected override float cooldown {get {return .35f;}}

    protected override void InstantiatePrefab() {
        crescent = Instantiate(crescentPrefab, source.transform.position, Quaternion.identity);
        crescent.GetComponent<AbilityPrefab>().source = source;
        crescent.GetComponent<AbilityPrefab>().knockbackPower = 10f;
        ModifyMeleeSize(crescent);
        crescent.transform.up = source.GetComponent<Entity>().castDirection;
        crescent.transform.position += new Vector3(source.castDirection.x, source.castDirection.y, 0);
    }

    public override void Cast(Transform source, int abilityLevel) {
        base.Cast(source, abilityLevel);
        if (OnCooldown()) return;

        source.Find("Pivot").Find("Weapon Pivot").GetComponent<Animator>().SetTrigger("Swing");
        InstantiatePrefab(); 
        StartCooldown();
       
    }

    protected override void Update() {
        base.Update();
    }

}
