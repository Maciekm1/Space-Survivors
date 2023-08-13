using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float healthMax = 100f;
    protected float healthCurrent;

    [SerializeField] protected float shieldMax = 50f;
    protected float shieldCurrent;

    [SerializeField] protected float healthRegen = 0.25f;

    [SerializeField] protected float shieldRegen = 0.5f;
    [SerializeField] protected float timeBtwnShieldRegen = 5f;
    protected float internalShieldRegenTimer = 0f;
    protected bool alive;

    // Events
    public Action OnHealthGain;
    public Action OnHealthLoss;
    public Action OnLoseAllHealth;

    public Action OnShieldGain;
    public Action OnShieldLoss;
    public Action OnLoseAllShield;

    public float GetHealthCurrent() { return healthCurrent; }
    public float GetHealthMax() { return healthMax; }
    public float GetShieldCurrent() { return shieldCurrent; }
    public float GetShieldMax() { return shieldMax; }

    protected virtual void Awake()
    {
        healthCurrent = healthMax;
        shieldCurrent = shieldMax;
        alive = true;

        internalShieldRegenTimer = timeBtwnShieldRegen;
    }

    protected virtual void Update()
    {
        internalShieldRegenTimer -= Time.deltaTime;
        if (healthCurrent <= 0 && alive) { 
            OnLoseAllHealth?.Invoke();
            alive = false;
        }
        if (shieldCurrent <= 0) { OnLoseAllShield?.Invoke(); }
        if (healthCurrent < healthMax) { healthCurrent += healthRegen * Time.deltaTime; }
        if (shieldCurrent < shieldMax && internalShieldRegenTimer <= 0) { shieldCurrent += shieldRegen * Time.deltaTime; }
    }

    public virtual void LoseHealth(float amount)
    {
        healthCurrent -= amount;
        OnHealthLoss?.Invoke();
    }

    public virtual void LoseShield(float amount)
    {
        shieldCurrent -= amount;
        OnShieldLoss?.Invoke();
    }

    public virtual void TakeDamage(float amount)
    {
        // Reset Shield Regen Timer
        internalShieldRegenTimer = timeBtwnShieldRegen;

        if (shieldCurrent > 0)
        {
            LoseShield(amount);
            return;
        }
        LoseHealth(amount);
    }

    public virtual void GainHealth(float amount)
    {
        shieldCurrent += amount;
        OnHealthGain?.Invoke();
    }

    public virtual void GainShield(float amount)
    {
        shieldCurrent += amount;
        OnShieldGain?.Invoke();
    }
}
