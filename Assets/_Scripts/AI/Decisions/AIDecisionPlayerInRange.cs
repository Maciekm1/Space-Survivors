using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName ="AI/AIDecision/PlayerInRange")]
public class AIDecisionPlayerInRange : AIDecision
{
    [SerializeField] private float range;
    public override bool Decide(GameObject go)
    {
        Vector2 towardsPlayer = PlayerController.Instance.transform.position - go.transform.position;
        if (towardsPlayer.magnitude <= range)
        {
            return true;
        }
        return false;
    }
}
