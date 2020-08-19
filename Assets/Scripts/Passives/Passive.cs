using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : MonoBehaviour
{
    protected abstract bool isStatusEffect();
    ///<summary> An effect will happen every time an x number of seconds pass, defined by tickSpeed. 
    /// If left at 0, it means that there is no tick effect. </summary>
    protected abstract float tickSpeed();
    protected float tickTimeElapsed = 0f;
    ///<summary> If this is a status effect, duration determines how long it will last in seconds. 
    /// If this is passive, then duration has no effect (Infinite duration).</summary>
    protected float duration = 5f;
    [HideInInspector] public Entity owner;
    protected abstract void TickEffect();
    public void UpdateTimeElapsed()
    {
        if (tickSpeed() > 0)
        {
            tickTimeElapsed += Time.deltaTime;
            if (tickTimeElapsed >= tickSpeed())
            {
                TickEffect();
                tickTimeElapsed = 0;
            }
        }
        if (isStatusEffect()) {
            duration -= Time.deltaTime;
            if (duration <= 0f) {
                Destroy(gameObject);
            }
        }
    }
}
