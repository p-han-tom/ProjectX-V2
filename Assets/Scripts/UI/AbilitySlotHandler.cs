using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotHandler : SlotHandler
{
    Image timerFill;
    protected override void Start()
    {
        base.Start();
        slotType = SlotHandler.SlotType.Active;
        timerFill = transform.Find("Timer Fill").GetComponent<Image>();
    }
    void Update()
    {
        UpdateTimerFill();
    }
    void UpdateTimerFill()
    {
        if (itemObject != null)
        {
            Active ability = itemObject.GetComponent<Active>();
            float currentCooldown = ability.GetCurrentCooldown();
            if (currentCooldown > 0)
            {
                timerFill.fillAmount = currentCooldown / ability.GetCooldown();
            }
            else
            {
                timerFill.fillAmount = 0;
            }
        }
    }
}
