using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawnpoint : MonoBehaviour
{
    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Structure")) {
            canSpawn = false;
        }
    }

}
