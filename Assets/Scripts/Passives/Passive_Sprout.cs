using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_Sprout : Passive
{
    protected override float tickSpeed() => 0f;
    protected override bool isStatusEffect() => false;
    
    float meleeModifier = 1f;
    public override void OnEquip() {
        owner.modMeleeAttackSize.FlatModifierAdd(meleeModifier);
    }
    public override void OnUnequip() {
        owner.modMeleeAttackSize.FlatModifierAdd(-meleeModifier);
    }
}
