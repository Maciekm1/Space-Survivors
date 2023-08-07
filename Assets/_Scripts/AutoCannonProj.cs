using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCannonProj : Projectile
{
    private Vector2 shotStrength;
    private float lifetimeEnd;

    private bool coroutineStarted = false;

    private void OnEnable()
    {
        coroutineStarted = false;
        spriteRenderer.enabled = true;
        // In the direction of Y-axis of proj
        Vector2 shotStrenghtNoPlayer = transform.up * projectileSpeed;

        // speed affected by player velocity, can't be lower than shotStrengthNoPlayer (i.e base speed)
        shotStrength = shotStrenghtNoPlayer * Mathf.Clamp(PlayerController.Instance.Rb.velocity.magnitude * playerVelocityMultiplier, 1f, PlayerController.Instance.Rb.velocity.magnitude * playerVelocityMultiplier);
        rb.AddForce(shotStrength, ForceMode2D.Impulse);

        lifetimeEnd = Time.time + projectileLifeTime;
    }

    private void OnDisable()
    {
        transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if(Time.time > lifetimeEnd)
        {
            if (usesParticleExplosion) 
            {
                if(!coroutineStarted)
                {
                    StartCoroutine(DeactivateObject());
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator DeactivateObject()
    {
        coroutineStarted = true;
        rb.velocity = Vector2.zero;
        spriteRenderer.enabled = false;
        particleSystemExplosion.Play();
        yield return new WaitForSeconds(particleSystemExplosion.main.duration);
        gameObject.SetActive(false);

    }
}
