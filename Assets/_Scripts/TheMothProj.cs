using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMothProj : Projectile
{
    private float colliderStartRadius;

    protected override void Awake()
    {
        base.Awake();
        colliderStartRadius = GetComponent<CircleCollider2D>().radius;
    }
    public override void Shoot()
    {
        base.Shoot();
        Vector2 direction = PlayerController.Instance.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        // In the direction of Y-axis of proj
        Vector2 towardsPlayer = PlayerController.Instance.transform.position - gameObject.transform.position;
        Vector2 shotStrength = towardsPlayer.normalized * projectileSpeed;
        //Rb.velocity = Vector2.zero;
        Rb.AddForce(shotStrength, ForceMode2D.Impulse);
    }

    public override void DestroyProjectile()
    {
        if (usesParticleExplosion)
        {
            if (!coroutineStarted)
            {
                StartCoroutine(this.DeactivateObject());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected new IEnumerator DeactivateObject()
    {
        coroutineStarted = true;
        Rb.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().radius = colliderStartRadius * 15;
        Invoke(nameof(disableCollider), 0.05f);
        spriteRenderer.enabled = false;
        particleSystemExplosion.Play();
        yield return new WaitForSeconds(particleSystemExplosion.main.duration);
        GetComponent<CircleCollider2D>().radius = colliderStartRadius;
        gameObject.SetActive(false);
    }

    private void disableCollider()
    {
        col.enabled = false;
    }
}
