using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public LayerMask sourceLayer;

    private Rigidbody2D rb;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(sourceLayer.value, gameObject.layer);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(sourceLayer.value, gameObject.layer);
        rb.velocity = direction*speed;
    }

    
}
