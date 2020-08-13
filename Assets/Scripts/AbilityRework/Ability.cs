using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]

public abstract class Ability : ScriptableObject
{
    public new string name = "New Ability";
    public abstract float GetCooldown();
    public abstract float GetDamage();

    public abstract void Cast(Vector2 direction, Vector3 mousePos, Transform source);

}
