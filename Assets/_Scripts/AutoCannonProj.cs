using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCannonProj : Projectile
{
    [SerializeField] protected PlayerWeapon weapon;
    [Range(0, 1)][SerializeField] protected float playerVelocityMultiplier;

    protected void OnEnable()
    {
        Shoot();
        // In the direction of Y-axis of proj
        // speed affected by player velocity, can't be lower than shotStrengthNoPlayer (i.e base speed)
        Vector2 shotStrength = transform.up * projectileSpeed * Mathf.Clamp(PlayerController.Instance.Rb.velocity.magnitude * playerVelocityMultiplier, 1f, PlayerController.Instance.Rb.velocity.magnitude * playerVelocityMultiplier);
        Rb.AddForce(shotStrength, ForceMode2D.Impulse);
    }
}
