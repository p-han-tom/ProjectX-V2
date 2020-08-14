using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Ability", menuName = "Projectile Ability")]
public class ProjectileAbility : Ability
{
    [Header("Projectile values")]
    public float speed;
    public GameObject onHitParticles;

    public override void Cast(Vector2 direction, Vector3 mousePos, Transform source) {
        ProjectileBehaviour projectileBehaviour = prefab.GetComponent<ProjectileBehaviour>();

        Instantiate(prefab, source.position, Quaternion.identity);
        projectileBehaviour.sourceLayer = source.gameObject.layer;
        projectileBehaviour.direction = direction;
        projectileBehaviour.speed = speed;

    }
}
