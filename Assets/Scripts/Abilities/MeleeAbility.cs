using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Ability", menuName = "Ability/Melee")]
public class MeleeAbility : Ability
{
    [Header("Melee values")]
    public float distanceFromCaster;
    public override void Cast(Vector2 direction, Vector3 mousePos, Transform source)
    {
        GameObject prefabInstance = Instantiate(prefab, source.position, Quaternion.identity);
        MeleeBehaviour meleeBehaviour = prefabInstance.GetComponent<MeleeBehaviour>();
        meleeBehaviour.RotateTowards(direction);
        prefabInstance.transform.position += distanceFromCaster * new Vector3(direction.x, direction.y, source.position.z);
    }
}
