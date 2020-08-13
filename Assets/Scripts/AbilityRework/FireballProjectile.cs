using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    // Travel path
    public Vector2 direction;
    public float speed = 20f;

    // Components
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*speed;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    } 
}
