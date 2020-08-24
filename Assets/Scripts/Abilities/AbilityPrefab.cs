using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityPrefab : MonoBehaviour
{
    // General
    [HideInInspector] public Entity source; 

    // On hit values
    [HideInInspector] public float damage;
    [HideInInspector] public float knockbackPower;
    protected float knockbackDuration = 0.15f;
    protected float recoveryDuration = 0.05f;

    protected LayerMask sourceLayer;

    protected virtual void Start()
    {
        sourceLayer = source.gameObject.layer;
        Physics2D.IgnoreLayerCollision(sourceLayer.value, gameObject.layer);
    }

    protected virtual void OnHit(Transform other) {}

    protected IEnumerator Knockback(Rigidbody2D other) {

        other.GetComponent<Entity>().underAttack = true;
        other.velocity = Vector2.zero;

        // Send initial force
        Vector2 difference = other.transform.position - source.transform.position;
        difference = difference.normalized * knockbackPower;
        other.GetComponent<Entity>().Knockback(difference);
        yield return new WaitForSeconds(knockbackDuration);

        // Stop movement for smooth recovery
        other.velocity = Vector2.zero;
        yield return new WaitForSeconds(recoveryDuration);

        // Enable movement
        other.GetComponent<Entity>().underAttack = false;
        Destroy(gameObject);

    }

    protected virtual void DestroyIfMissed()
    {
        Destroy(gameObject, knockbackDuration*2+recoveryDuration);
    }


}
