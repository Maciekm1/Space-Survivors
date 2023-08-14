using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjAsteroidMini : Projectile
{
    [SerializeField] private float targetDelay;
    [SerializeField] private float accelerationStrength;
    [SerializeField] private float accelerateTimer;
    private float internalAccelerateTimer;
    private float internalTargetDelay;
    [SerializeField] AnimationClip deathAnimationClip;

    private void Start()
    {
        internalTargetDelay = targetDelay;
        internalAccelerateTimer = accelerateTimer;
        col.enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        internalAccelerateTimer -= Time.deltaTime;
        internalTargetDelay -= Time.deltaTime;
        if(!shot && internalTargetDelay <= 0)
        {
            Shoot();
        }
        if(internalAccelerateTimer <= 0 && shot)
        {
            Accelerate();
            internalAccelerateTimer = accelerateTimer;
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        // In the direction of Y-axis of proj
        Vector2 towardsPlayer = PlayerController.Instance.transform.position - gameObject.transform.position;
        Vector2 shotStrength = towardsPlayer.normalized * projectileSpeed;
        //Rb.velocity = Vector2.zero;
        Rb.AddForce(shotStrength, ForceMode2D.Impulse);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        internalTargetDelay = targetDelay;
        shot = false;
    }

    private void Accelerate()
    {
        Rb.AddForce(Rb.velocity.normalized * accelerationStrength, ForceMode2D.Impulse);
    }

    public override void DestroyProjectile()
    {
        if (usesParticleExplosion)
        {
            if (!coroutineStarted)
            {
                StartCoroutine(DeactivateAsteroid());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateAsteroid()
    {
        coroutineStarted = true;
        Rb.velocity = Vector2.zero;
        col.enabled = false;
        //spriteRenderer.enabled = false;
        GetComponentInChildren<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(deathAnimationClip.length);
        gameObject.SetActive(false);
    }
}
