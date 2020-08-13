using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{

    [SerializeField] private Ability ability;
    private float cooldownTimer;
    private float cooldownDuration;
    private float damage;

    Vector2 direction;
    Vector3 mousePos;
    

    void Start()
    {
        cooldownTimer = ability.GetCooldown();
        cooldownDuration = 0;
        damage = ability.GetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2 (mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;

        if (cooldownTimer > 0) {
            cooldownTimer-=Time.deltaTime;
        }

        if (Input.GetMouseButton(0)) {
            ability.Cast(direction, mousePos, transform);

        }
    }

}
