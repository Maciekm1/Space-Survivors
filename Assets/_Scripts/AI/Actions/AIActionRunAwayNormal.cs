using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIAction/AIActionRunAwayNormal")]
public class AIActionRunAwayNormal : AIAction
{

    public override void Do(GameObject go)
    {

    }
    public override void DoFixed(GameObject go)
    {
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            Vector2 normal = new Vector2(-enemy.TowardsPlayer.normalized.y, enemy.TowardsPlayer.normalized.x);
            Vector2 oppositeNormal = new Vector2(normal.y, -normal.x);

            Vector2 desiredVelocity = normal * enemy.MoveSpeed;
            Vector2 currentVelocity = go.GetComponent<Rigidbody2D>().velocity;

            Vector2 forceToAdd = desiredVelocity - currentVelocity;

            go.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Force);
        }
    }
}
