using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Minor/StatCard")]
public class StatCard : Card
{
    [System.Serializable]
    public class StatIncrease
    {
        public Enums.StatType statType;
        public float increaseAmount;
    }

    public List<StatIncrease> statIncreases = new List<StatIncrease>();

    public override void ApplyEffect(PlayerController playerController)
    {
        base.ApplyEffect(playerController);

        // Apply the stat increases to the PlayerStats
        foreach (StatIncrease increase in statIncreases)
        {
            switch (increase.statType)
            {
                case Enums.StatType.DamagePerBulletMult:
                    playerController.GetPlayerStats().damagePerBulletMult += increase.increaseAmount;
                    break;
                case Enums.StatType.FireRateMult:
                    playerController.GetPlayerStats().fireRateMult += increase.increaseAmount;
                    break;
                case Enums.StatType.ProjectileSpeedMult:
                    playerController.GetPlayerStats().projectileSpeedMult += increase.increaseAmount;
                    break;
                case Enums.StatType.ProjectileLifeTimeMult:
                    playerController.GetPlayerStats().projectileLifeTimeMult += increase.increaseAmount;
                    break;
                case Enums.StatType.ProjectileKnockBackMult:
                    playerController.GetPlayerStats().projectileKnockBackMult += increase.increaseAmount;
                    break;
                case Enums.StatType.WeaponRecoilMult:
                    playerController.GetPlayerStats().weaponRecoilMult += increase.increaseAmount;
                    break;
                case Enums.StatType.MoveSpeed:
                    playerController.GetPlayerStats().moveSpeed += increase.increaseAmount;
                    break;
                case Enums.StatType.RotationSpeed:
                    playerController.GetPlayerStats().rotationSpeed += increase.increaseAmount;
                    break;
                case Enums.StatType.DashForce:
                    playerController.GetPlayerStats().dashForce += increase.increaseAmount;
                    break;
                case Enums.StatType.DashCharges:
                    playerController.GetPlayerStats().dashCharges += (int)increase.increaseAmount;
                    break;
                case Enums.StatType.DashRegen:
                    playerController.GetPlayerStats().dashRegen += increase.increaseAmount;
                    break;
                case Enums.StatType.Health:
                    playerController.GetPlayerStats().health += increase.increaseAmount;
                    break;
                case Enums.StatType.HealthRegen:
                    playerController.GetPlayerStats().healthRegen += increase.increaseAmount;
                    break;
                case Enums.StatType.Shield:
                    playerController.GetPlayerStats().shield += increase.increaseAmount;
                    break;
                case Enums.StatType.ShieldRegen:
                    playerController.GetPlayerStats().shieldRegen += increase.increaseAmount;
                    break;
                case Enums.StatType.ExpGainMult:
                    playerController.GetPlayerStats().expGainMult += increase.increaseAmount;
                    break;
            }
        }
    }
}
