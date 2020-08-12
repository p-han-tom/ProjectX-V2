using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : ItemEffect
{
    void Start()
    {
        StartConsumable();
    }
    public abstract void Consume();
    protected abstract void StartConsumable();
}
