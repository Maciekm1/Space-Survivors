using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIAction/AIActionRunAway")]
public class AIActionRunAway : AIAction
{

    public override void Do(GameObject go)
    {
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TowardsPlayer = PlayerController.Instance.transform.position - go.transform.position;
        }
    }
    public override void DoFixed(GameObject go)
    {
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            Vector2 desiredVelocity = -enemy.TowardsPlayer.normalized * enemy.MoveSpeed;
            Vector2 currentVelocity = go.GetComponent<Rigidbody2D>().velocity;

            Vector2 forceToAdd = desiredVelocity - currentVelocity;

            go.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Force);
        }
    }
}
