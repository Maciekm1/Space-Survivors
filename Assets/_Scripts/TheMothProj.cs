using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMothProj : Projectile
{
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
}
