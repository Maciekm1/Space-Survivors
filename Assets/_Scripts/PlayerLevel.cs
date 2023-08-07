using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int playerStartLevel = 0;
    private int playerCurrentLevel;

    [SerializeField] private float playerXPRequirementPerLevel = 5f;
    [SerializeField] private float playerXPRequirementScalePerLevel = 1.5f;

    private float playerCurrentExperience;
    private float playerXPNeededForLevelUp;

    [SerializeField] private float playerXPGainMult = 1f;

    //Events
    private event Action OnXPGain;
    private event Action OnLevelUp;

    private void Awake()
    {
        playerCurrentLevel = playerStartLevel;
        playerXPNeededForLevelUp = playerXPRequirementPerLevel;
    }

    public void PlayerGainXP(float xp)
    {
        playerCurrentExperience += xp * playerXPGainMult;
        OnXPGain?.Invoke();

        if(playerCurrentExperience >=  playerXPNeededForLevelUp) { PlayerLevelUp(playerCurrentExperience - playerXPNeededForLevelUp); }
    }

    public void PlayerLevelUp(float overload)
    {
        playerCurrentLevel++;
        playerCurrentExperience = overload;
        playerXPNeededForLevelUp = playerXPRequirementPerLevel * playerXPRequirementScalePerLevel * playerCurrentLevel;
        OnLevelUp?.Invoke();
        PlayerUI.instance.UpdatePlayerLevel();
    }

    public int GetPlayerLevel() {  return playerCurrentLevel; }
    public float GetPlayerXP() { return playerCurrentExperience; }
    public float GetPlayerXPRequirement() { return playerXPNeededForLevelUp; }
}
