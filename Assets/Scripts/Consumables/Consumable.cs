using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    void Start()
    {
        CustomStart();
    }
    public abstract void Consume();
    protected abstract void CustomStart();
}
