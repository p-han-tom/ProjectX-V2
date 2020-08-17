using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItems : ActiveItems
{

    Vector2 direction;
    Vector3 mousePos;
    KeyCode[] playerKeybindings = {KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.LeftShift, KeyCode.R};

    protected override void GetAbilityInput() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;

        for (int i = 0; i < 4; i ++) {
            if (Input.GetKeyDown(playerKeybindings[i]) && items[i] != null) {
                triggered = true;
                abilityIndex = i;
                break;
            }
        }
    }

    protected override IEnumerator Cast(float castTime, int index) {
        yield return new WaitForSeconds(castTime);
        items[index].GetAbility().Cast(direction, mousePos, transform, items[index].GetAbilityLevel());
    }

    protected override void Charge(float castTime, int index) {
        transform.Find("Pivot").Find("Pivot").Find("HeldWeapon").GetComponent<SpriteRenderer>().sprite = items[index].GetAbility().chargedSprite;
        if (Input.GetKeyUp(playerKeybindings[index])) {
            items[index].GetAbility().Cast(direction, mousePos, transform, items[index].GetAbilityLevel());
            triggered = false;
        }
    }
}
