using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour 
{

    [SerializeField]
    private Health _health;


    public void TakeDamage(float amount, AbilityWeaknesses damageType)
    {
        _health.AddHealth(-amount, damageType);
    }

    public void SetInvincibile(bool isInvincible)
    {
        _health.SetInvincible(isInvincible);
    }

    public void SetInvincibile(bool isInvincible, float duration)
    {
        _health.SetInvincible(isInvincible, duration);
    }

}
