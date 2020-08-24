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

    protected LayerMask sourceLayer;

    protected virtual void Start()
    {
        sourceLayer = source.gameObject.layer;
        Physics2D.IgnoreLayerCollision(sourceLayer.value, gameObject.layer);
    }

    protected virtual void OnHit(Transform other) {}

    protected IEnumerator Knockback(Rigidbody2D other) {

        // Prevent movement and wait for knockback duration
        other.GetComponent<Entity>().underAttack = true;

        // Send initial force
        Vector2 difference = other.transform.position - source.transform.position;
        difference = difference.normalized * knockbackPower;
        other.GetComponent<Entity>().Knockback(difference);

        
        yield return new WaitForSeconds(0.05f);

        // // Stop movement for smooth recovery
        // other.velocity = Vector2.zero;
        // yield return new WaitForSeconds(0.05f);

        // Enable movement
        other.GetComponent<Entity>().underAttack = false;
        Destroy(gameObject);

    }


}
