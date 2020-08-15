using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : MonoBehaviour
{

    public GameItem[] items = new GameItem[4];

    // Player
    KeyCode[] playerKeybindings = {KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.LeftShift, KeyCode.R};

    // Cast ability
    bool triggered;
    int abilityIndex;
    float chargeTimer;

    // Mouse
    Vector2 direction;
    Vector3 mousePos;

    void Update()
    {
        GetAbilityInput();
        CheckAbilityTrigger();
    }

    void GetAbilityInput() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;

        if (gameObject.CompareTag("Player")) {

            for (int i = 0; i < 4; i ++) {
                if (Input.GetKeyDown(playerKeybindings[i]) && items[i] != null) {
                    triggered = true;
                    abilityIndex = i;
                    break;
                }
            }

        } else if (gameObject.CompareTag("Enemy")) {

            // If player in range select random ability to use

        }
    }

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

    IEnumerator Cast(float castTime, int index) {
        yield return new WaitForSeconds(castTime);
        items[index].GetAbility().Cast(direction, mousePos, transform, items[index].GetAbilityLevel());
    }


    void Charge (float castTime, int index) {
        if (gameObject.CompareTag("Player")) {
            if (Input.GetKeyUp(playerKeybindings[index])) {
                items[index].GetAbility().Cast(direction, mousePos, transform, items[index].GetAbilityLevel());
                triggered = false;
            }
        } else if (gameObject.CompareTag("Enemy")) {
            StartCoroutine(Cast(castTime, index));
        }
    }
    
}
