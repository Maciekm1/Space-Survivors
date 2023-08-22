using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    //Damage and Projectiles
    public float damagePerBulletMult;
    public float fireRateMult;
    public float projectileSpeedMult;
    public float projectileLifeTimeMult;
    public float projectileKnockBackMult;
    public float weaponRecoilMult;

    // Speed and Agility
    public float moveSpeed;
    public float rotationSpeed;
    public float dashForce;
    public float dashCharges;
    public float dashRegen;

    // Health and Shield
    public float health;
    public float healthRegen;
    public float shield;
    public float shieldRegen;

    // Misc
    public float expGainMult;

}
