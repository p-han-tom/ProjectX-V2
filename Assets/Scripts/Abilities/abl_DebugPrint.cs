using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abl_DebugPrint : Ability
{
    protected override void SetVariables() {
        cooldown = 4f;
    }
    protected override void Cast() {
        Debug.Log("abl_DebugPrint has been cast");
    }
}
