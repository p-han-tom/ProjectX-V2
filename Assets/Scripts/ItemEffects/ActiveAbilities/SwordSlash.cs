using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : ActiveAbility
{
    protected override void StartActiveAbility() {
        name = "SwordSlash";
        cooldown = 0f;
    }
    public override void Cast() {
        Debug.Log("SLASHED!");
    }
}
