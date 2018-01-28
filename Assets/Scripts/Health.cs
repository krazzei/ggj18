using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
    public float CurrentHealth { get { return ApplyModifiersToHealthValues(_currentHealth); } }
    public float MaxHealth { get { return ApplyModifiersToHealthValues(_maxHealth); } }
    public bool IsInvincible { get { return _isInvincible || _isInvincibleWithDuration; } }


    [SerializeField]
    private float _currentHealth = 0.0f;
    [SerializeField]
    private float _maxHealth = 100.0f;
    [SerializeField]
    private float _hitCooldown = 3f;
    [SerializeField]
    private bool _isInvincible = false;
    private bool _isInvincibleWithDuration = false;
    [SerializeField]
    private float _invincibleDuration = 3f;
    [SerializeField]
    private ModifierQueue _modifierQueue;

    private float _invincibleStartTime;
    private float _lastHitTime;

	// Use this for initialization
	void Start () 
    {
        _modifierQueue.OnModAdded += OnModAdded;
        _modifierQueue.OnModRemoved -= OnModRemoved;
	}

    private void OnModAdded(Modifier.ModifierData modData)
    {
        // Not sure if need right now. Values are being calculated on the fly...
    }

    private void OnModRemoved(Modifier.ModifierData modData)
    {
        // Not sure if need right now. Values are being calculated on the fly...
    }

    private float ApplyModifiersToHealthValues(float health)
    {
        var mods = _modifierQueue.GetAllModifierData();
        float newValue = health;
        foreach(var mod in mods)
        {
            newValue += newValue * mod.healthPercentage;
        }
        return newValue;
    }

    public void AddHealth(float amount, AbilityWeaknesses damageType)
    {
        var amountAfterProcessing = 0f;
        if(amount < 0)
        {
            if(!IsInvincible)
            {
                _lastHitTime = Time.time;
                amountAfterProcessing = ProcessIncomingDamage(amount, damageType);
                TookDamage(amount);
            }
        }
        else
        {
            amountAfterProcessing = ProcessIncomingHealth(amount, damageType);
            WasHealed(amount);
        }
        _currentHealth += amountAfterProcessing;
    }

    public void SetInvincible(bool isInvincible) {
        _isInvincible = isInvincible;
        if(_isInvincible)
        {
            InvincibilityStart();
        }
        else
        {
            InvincibilityEnd();
        }
    }

    public void SetInvincible(bool isInvincible, float duration) {
        _isInvincibleWithDuration = true;
        _invincibleStartTime = Time.time;
        _invincibleDuration = duration;
        SetInvincible(isInvincible);
    }

	// Update is called once per frame
	void Update () 
    {
        if(_isInvincibleWithDuration)
        {
            if(_invincibleStartTime + _invincibleDuration < Time.time)
            {
                _isInvincibleWithDuration = false;
                _isInvincible = false;
                InvincibilityEnd();
            }
        }

        // Hit cooldown. Don't let a player get hit repeatedly within this amount of time
        if(_lastHitTime + _hitCooldown > Time.time)
        {
            _isInvincible = true;
        }
        else
        {
            _isInvincible = false;
        }
	}

    // Hook for doing damage calculations from modifiers
    protected virtual float ProcessIncomingDamage(float amount, AbilityWeaknesses damageType)
    {
        float adjustedAmount = amount;
        foreach (var mod in _modifierQueue.GetAllModifierData())
        {
            if(((int)mod.weakness & (int)damageType) > 0 || (mod.weakness == AbilityWeaknesses.All))
            {
                adjustedAmount += adjustedAmount * mod.damageTakenPercentage;
            }
        }

        return adjustedAmount;
    }

    // Hook for doing health gained calculations from modifiers
    protected virtual float ProcessIncomingHealth(float amount, AbilityWeaknesses damageType)
    {
        float adjustedAmount = amount;
        foreach(var mod in _modifierQueue.GetAllModifierData())
        {
            if (((int)mod.weakness & (int)damageType) > 0 || (mod.weakness == AbilityWeaknesses.All))
            {
                adjustedAmount += adjustedAmount * mod.healthGrantedPercentage;
            }
        }
        return adjustedAmount;
    }

    // Hook for displaying text or showing effects etc
    protected virtual void TookDamage(float amount)
    {
        
    }

    // Hook for displaying text or showing effects etc

    protected virtual void WasHealed(float amount)
    {
        
    }

    // Hook for displaying text or showing effects etc
    protected virtual void InvincibilityStart()
    {

    }

    // Hook for displaying text or showing effects etc
    protected virtual void InvincibilityEnd()
    {

    }

}
