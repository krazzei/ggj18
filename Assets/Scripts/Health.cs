using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
    public float CurrentHealth { get { return _currentHealth; } }
    public float MaxHealth { get { return _maxHealth; } }
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

    private float _invincibleStartTime;
    private float _lastHitTime;

	// Use this for initialization
	void Start () 
    {
		
	}

    public void AddHealth(float amount)
    {
        var amountAfterProcessing = 0f;
        if(amount < 0)
        {
            if(!IsInvincible)
            {
                _lastHitTime = Time.time;
                amountAfterProcessing = ProcessIncomingDamage(amount);
                TookDamage(amount);
            }
        }
        else
        {
            amountAfterProcessing = ProcessIncomingHealth(amount);
            WasHealed(amount);
        }
        _currentHealth += amount;
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
    protected virtual float ProcessIncomingDamage(float amount)
    {
        return amount;
    }

    // Hook for doing health gained calculations from modifiers
    protected virtual float ProcessIncomingHealth(float amount)
    {
        return amount;
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
