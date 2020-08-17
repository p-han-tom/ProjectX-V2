using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Ability", menuName = "Ability/Projectile")]
public class ProjectileAbility : Ability
{
    [Header("Projectile values")]
    public float speed;
    public GameObject onHitParticles;

    public override void Cast(Vector2 direction, Vector3 mousePos, Transform source, int abilityLevel)
    {
        base.Cast(direction, mousePos, source, abilityLevel);
        GameObject prefabInstance = Instantiate(prefab, source.position, Quaternion.identity);
        ProjectileBehaviour projectileBehaviour = prefabInstance.GetComponent<ProjectileBehaviour>();

        projectileBehaviour.RotateTowards(direction);
        projectileBehaviour.sourceLayer = source.gameObject.layer;
        projectileBehaviour.direction = direction;
        projectileBehaviour.speed = speed;

        EquipSprite(source);
    }
}
