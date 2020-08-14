using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{

    [Header("Essentials")]
    public new string name;
    public float cooldown;
    public float castTime;
    public float damage;
    public AbilityType abilityType;
    public enum AbilityType {IsCharged, IsCast, IsToggled}

    public GameObject prefab;



    public abstract void Cast(Vector2 direction, Vector3 mousePos, Transform source);

}
