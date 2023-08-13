using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName ="AI/AIDecision/PlayerWithinRange")]
public class AIDecisionPlayerWithinRange : AIDecision
{
    public float minRange;
    public float maxRange;
    public override bool Decide(GameObject go)
    {
 
        Vector2 towardsPlayer = PlayerController.Instance.transform.position - go.transform.position;
        if (towardsPlayer.magnitude <= maxRange && towardsPlayer.magnitude >= minRange)
        {
            return true;
        }
        return false;
    }
}
