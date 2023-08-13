using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private float playerHealthGainMult = 1f;
    [SerializeField] private float playerShieldGainMult = 1f;
    [SerializeField] private float playerHealthLossMult = 1f;
    [SerializeField] private float playerShieldLossMult = 1f;

    public override void LoseHealth(float amount) 
    {
        healthCurrent -= amount * playerHealthLossMult;
        OnHealthLoss?.Invoke();
    }

    public override void LoseShield(float amount)
    {
        shieldCurrent -= amount * playerShieldLossMult;
        OnShieldLoss?.Invoke();
    }

    public override void GainHealth(float amount)
    {
        shieldCurrent += amount * playerHealthGainMult;
        OnHealthGain?.Invoke();
    }

    public override void GainShield(float amount)
    {
        shieldCurrent += amount * playerShieldGainMult;
        OnShieldGain?.Invoke();
    }

}
