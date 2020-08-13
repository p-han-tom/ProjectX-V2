using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected float cooldown;
    protected float cooldownElapsed = 0f;
    void Start()
    {
        SetVariables();
        CustomStart();
    }
    void Update()
    {
        CustomUpdate();
        UpdateCooldown();
    }

    protected void GoOnCooldown()
    {
        cooldownElapsed = cooldown;
    }
    public void AttemptCast()
    {
        if (cooldownElapsed <= 0)
        {
            Debug.Log("Ability casted");
            Cast();
        }
        else
        {
            Debug.Log("Ability has " + (cooldown - cooldownElapsed) + "s remaining");
        }
    }
    private void UpdateCooldown()
    {
        if (cooldownElapsed > 0f) cooldownElapsed -= Time.deltaTime;
    }
    protected abstract void Cast();
    protected abstract void SetVariables();
    protected virtual void CustomStart() { }
    protected virtual void CustomUpdate() { }
}
