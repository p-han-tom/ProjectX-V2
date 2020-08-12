using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ItemEffect
{
    protected float cooldown;
    void Start()
    {
        StartAbility();
    }
    public abstract void Cast();
    protected abstract void StartAbility();
}
