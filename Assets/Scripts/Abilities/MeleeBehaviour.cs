using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
    public void RotateTowards(Vector2 direction) {
        transform.up = direction;
    }
}
