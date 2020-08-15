using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    float statValue;
    float statFlatModifier = 0;
    float statFactorModifer = 1f;
    public Stat(float statValue)
    {
        this.statValue = statValue;
    }
    public float defaultValue() { return statValue; }
    public float value() { return (statValue + statFlatModifier) * statFactorModifer; }
    public void FlatModifierAdd(float increase) { statFlatModifier += increase; }
    //FactorModifierAdd should take in a decimal representing a percentage 
    public void FactorModifierAdd(float increase) { statFactorModifer += increase; }
}
