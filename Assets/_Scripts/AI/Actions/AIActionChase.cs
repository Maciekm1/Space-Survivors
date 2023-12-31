using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(menuName = "AI/AIAction/AIActionChase")]
public class AIActionChase : AIAction
{
    public override void Do(GameObject go)
    {

    }

    public override void DoFixed(GameObject go)
    {
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            Vector2 desiredVelocity = enemy.TowardsPlayer.normalized * enemy.MoveSpeed;
            Vector2 currentVelocity = go.GetComponent<Rigidbody2D>().velocity;

            Vector2 forceToAdd = desiredVelocity - currentVelocity;

            go.GetComponent<Rigidbody2D>().AddForce(forceToAdd, ForceMode2D.Force);
        }
    }
}
