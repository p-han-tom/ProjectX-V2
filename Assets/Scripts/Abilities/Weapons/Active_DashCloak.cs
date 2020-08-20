using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_DashCloak : Weapon
{
    [Header("Dash Trail Prefab")]
    public GameObject ghostTrail;
    
    protected override string name {get {return "Dash Cloak";}}
    protected override float cooldown {get {return 5f;}}

    float dashTimer = 0.075f;
    float dashTime = 0f;
    float dashForce = 1000f;
    Vector2 dashDirection;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // Dash if time > 0
        if (dashTime > 0f) {
            source.GetComponent<Rigidbody2D>().AddForce(dashDirection * dashForce);
            source.transform.localRotation = (dashDirection.x <= 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            dashTime -= Time.deltaTime;
        }
    }

    public override void Cast(Transform source, int abilityLevel) {
        base.Cast(source, abilityLevel);
        dashDirection = this.source.castDirection;
        dashTime = dashTimer;
    }
}
