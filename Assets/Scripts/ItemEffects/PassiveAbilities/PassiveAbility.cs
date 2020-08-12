using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : ItemEffect
{
    void Start()
    {
        StartPassiveAbility();
    }
    // public abstract void differentpassiveffects :|
    protected abstract void StartPassiveAbility();
}
