using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public abstract class Projectile : MonoBehaviour
{
    [field:SerializeField] public float ProjectileDamage { get; private set; }
    [field:SerializeField] public float projectileSpeed { get; private set; }
    [SerializeField] protected float projectileLifeTime;
    [field:SerializeField] public float ProjectileKnockback { get; private set; }
    [field:SerializeField] public bool playerDamage { get; private set; }

    [SerializeField] protected bool usesParticleExplosion;
    [SerializeField] protected ParticleSystem particleSystemExplosion;

    public Rigidbody2D Rb { get; private set; }
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;

    protected bool shot;
    protected bool coroutineStarted = false;
    protected float lifetimeEnd;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        col = Rb.GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lifetimeEnd = projectileLifeTime * PlayerController.Instance.GetPlayerStats().projectileLifeTimeMult;
    }

    protected virtual void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            lifetimeEnd -= Time.deltaTime;
        }
        if (lifetimeEnd  <= 0)
        {
            DestroyProjectile();
        }
    }

    public virtual void Shoot()
    {
        coroutineStarted = false;
        spriteRenderer.enabled = true;
        col.enabled = true;

        shot = true;
    }

    protected virtual void OnDisable()
    {
        transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        Rb.velocity = Vector2.zero;
        lifetimeEnd = projectileLifeTime * PlayerController.Instance.GetPlayerStats().projectileLifeTimeMult;
    }

    public virtual void DestroyProjectile()
    {
        if (usesParticleExplosion)
        {
            if (!coroutineStarted)
            {
                StartCoroutine(DeactivateObject());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected IEnumerator DeactivateObject()
    {
        coroutineStarted = true;
        Rb.velocity = Vector2.zero;
        col.enabled = false;
        spriteRenderer.enabled = false;
        particleSystemExplosion.Play();
        yield return new WaitForSeconds(particleSystemExplosion.main.duration);
        gameObject.SetActive(false);
    }
}
