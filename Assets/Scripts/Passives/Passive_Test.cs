using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_Test : Passive
{
    public GameObject prefab;
    protected override bool isStatusEffect() => false;
    protected override float tickSpeed() => 1f;

    protected override void TickEffect() {
        Instantiate(prefab, owner.transform.position, Quaternion.identity);
    }
}
