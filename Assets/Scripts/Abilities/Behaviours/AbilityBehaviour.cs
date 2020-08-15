using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehaviour : MonoBehaviour
{

    [HideInInspector] public KeyCode abilityKeycode;
    [HideInInspector] public bool abilityTriggered;
    [HideInInspector] public bool abilityCanStart = false;
    [HideInInspector] public bool channelBroken;

    [HideInInspector] public bool isChargedAbility;
    [HideInInspector] public bool isCastedAbility;
    [HideInInspector] public bool isChanneledAbility;
    [HideInInspector] public float castTime;


    float channelTimer = 0;

    void Update()
    {
        if (abilityTriggered && !abilityCanStart) {
            if (isCastedAbility) {
                StartCoroutine(Cast(castTime));
            } else if (isChargedAbility) {
                Charge();
            } else if (isChanneledAbility){
                Channel(castTime);
            }
            GetComponent<SpriteRenderer>().enabled = false;
        } else {
            GetComponent<SpriteRenderer>().enabled = true;
            CustomUpdate();
        }
    }

    protected virtual void CustomUpdate() {}

    protected IEnumerator Cast(float castTime) {
        yield return new WaitForSeconds(castTime);
        abilityCanStart = true;
    }

    protected void Charge() {
        if (Input.GetKeyUp(abilityKeycode)) {
            abilityCanStart = true;
            abilityTriggered = false;
        }
    }

    protected void Channel(float castTime) {
        channelTimer += Time.deltaTime;
        if (channelTimer > castTime) {
            abilityCanStart = true;
            abilityTriggered = false;
        }

        if (channelBroken) {
            channelTimer = 0;
            abilityTriggered = false;
        }
    }
}
