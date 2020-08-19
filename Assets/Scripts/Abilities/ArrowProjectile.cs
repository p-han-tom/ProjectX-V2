using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : AbilityPrefab
{
    [HideInInspector] public float speed;
    [HideInInspector] public Vector2 direction;
    bool hit;
    private Rigidbody2D rb;

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(sourceLayer.value, gameObject.layer);
        rb.velocity = direction*speed;
    }

    protected override void OnHit(Transform other) {
        if (other.GetComponent<Entity>() != null) {
            other.GetComponent<Entity>().TakeDamage(damage);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        
        transform.parent = other.transform;
        
        OnHit(other.collider.transform);
        Destroy(gameObject);
    }
    
}
