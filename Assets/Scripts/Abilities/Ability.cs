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

    public enum AbilityType {isCast, isCharged, IsToggled}
    public AbilityType abilityType;

    public enum AccessoryLocation {None, Weapon, Body, Helmet}
    public AccessoryLocation accessoryLocation;

    public Sprite sprite;
    public Sprite chargedSprite;
    public float spriteRotation;

    public GameObject prefab;

    public virtual void Cast(Vector2 direction, Vector3 mousePos, Transform source, int abilityLevel) {
        source.Find("Pivot").Find("Pivot").GetComponent<Animator>().SetTrigger("Swing");
        EquipSprite(source);
    }

    protected void EquipSprite(Transform source) {
        if (accessoryLocation != AccessoryLocation.None) {
            if (accessoryLocation == AccessoryLocation.Weapon) {
                source.Find("Pivot").Find("Pivot").Find("HeldWeapon").GetComponent<SpriteRenderer>().sprite = sprite;
                source.Find("Pivot").Find("Pivot").Find("HeldWeapon").transform.localRotation = Quaternion.Euler(0,0,spriteRotation);
            }
            // Implement others later
        }
    }

}
