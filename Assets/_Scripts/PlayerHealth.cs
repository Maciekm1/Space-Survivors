using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerHealthMax = 100f;
    private float playerHealthcurrent;

    [SerializeField] private float playerShieldMax = 50f;
    private float playerShieldcurrent;

    [SerializeField] private float playerHealthRegen = 0.25f;

    [SerializeField] private float playerShieldRegen = 0.5f;
    [SerializeField] private float timeBtwnShieldRegen = 5f;
    private float internalShieldRegenTimer = 0f;

    [SerializeField] private float playerHealthGainMult = 1f;
    [SerializeField] private float playerShieldGainMult = 1f;
    [SerializeField] private float playerHealthLossMult = 1f;
    [SerializeField] private float playerShieldLossMult = 1f;

    // Events
    private event Action OnPlayerHealthGain;
    private event Action OnPlayerHealthLoss;
    private event Action OnPlayerLoseAllHealth;

    private event Action OnPlayerShieldGain;
    private event Action OnPlayerShieldLoss;
    private event Action OnPlayerLoseAllShield;

    public float GetPlayerHealth() { return playerHealthcurrent; }
    public float GetPlayerHealthMax() { return playerHealthMax; }
    public float GetPlayerShield() { return playerShieldcurrent; }
    public float GetPlayerShieldMax() { return playerShieldMax; }

    private void Awake()
    {
        playerHealthcurrent = playerHealthMax;
        playerShieldcurrent = playerShieldMax;

        internalShieldRegenTimer = timeBtwnShieldRegen;
    }

    private void Update()
    {
        internalShieldRegenTimer -= Time.deltaTime;
        if(playerHealthcurrent <= 0) { OnPlayerLoseAllHealth?.Invoke(); }
        if(playerShieldcurrent <= 0) { OnPlayerLoseAllShield?.Invoke(); }
        if(playerHealthcurrent < playerHealthMax) { playerHealthcurrent += playerHealthRegen * Time.deltaTime; }
        if(playerShieldcurrent < playerShieldMax && internalShieldRegenTimer <= 0) { playerShieldcurrent += playerShieldRegen * Time.deltaTime; }
    }

    public void PlayerLoseHealth(float amount) 
    {
        playerHealthcurrent -= amount * playerHealthLossMult;
        OnPlayerHealthLoss?.Invoke();
    }

    public void PlayerLoseShield(float amount)
    {
        playerShieldcurrent -= amount * playerShieldLossMult;
        OnPlayerShieldLoss?.Invoke();
    }

    public void PlayerTakeDamage(float amount)
    {
        // Reset Shield Regen Timer
        internalShieldRegenTimer = timeBtwnShieldRegen;

        if (playerShieldcurrent > 0)
        {
            PlayerLoseShield(amount);
            return;
        }
        PlayerLoseHealth(amount);
    }

    public void PlayerGainHealth(float amount)
    {
        playerShieldcurrent += amount * playerHealthGainMult;
        OnPlayerHealthGain?.Invoke();
    }

    public void PlayerGainShield(float amount)
    {
        playerShieldcurrent += amount * playerShieldGainMult;
        OnPlayerShieldGain?.Invoke();
    }

}
