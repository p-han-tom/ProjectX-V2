using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItems : MonoBehaviour
{

    public GameItem[] items = new GameItem[4];

    // Cast ability
    protected bool triggered;
    protected int abilityIndex;
    protected float chargeTimer;


    void Update()
    {
        GetAbilityInput();
        CheckAbilityTrigger();
    }

    protected abstract void GetAbilityInput();

    void CheckAbilityTrigger() {

        if (triggered) {
            Ability ability = items[abilityIndex].GetAbility();

            switch ((int)(ability.abilityType)) {
                case 0:
                    StartCoroutine(Cast(ability.castTime, abilityIndex));
                    triggered = false;
                    break;
                case 1:
                    Charge(ability.castTime, abilityIndex);
                    break;
            }
        }
    }

    protected abstract IEnumerator Cast(float castTime, int index);
    protected abstract void Charge (float castTime, int index);
    
}
