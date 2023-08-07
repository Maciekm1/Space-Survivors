using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileLifeTime;

    [Range(0,1)][SerializeField] protected float playerVelocityMultiplier;

    [SerializeField] protected PlayerWeapon weapon;

    [SerializeField] protected bool usesParticleExplosion;
    [SerializeField] protected ParticleSystem particleSystemExplosion;

    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
