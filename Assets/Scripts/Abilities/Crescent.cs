using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crescent : AbilityPrefab
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnHit(Transform other) {

        if (other.GetComponent<Entity>() != null) {
            other.GetComponent<Entity>().TakeDamage(damage);
            StartCoroutine(Knockback(other.GetComponent<Rigidbody2D>()));
        }        
    }

    void OnTriggerEnter2D(Collider2D other) {
        OnHit(other.transform);
    }
}
