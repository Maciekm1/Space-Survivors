using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIDecision/EnemyBelowHealth")]
public class AIDecisionEnemyBelowHealth : AIDecision
{
    [Tooltip("Returns true if enemy's health value <= health")]
    public float health;
    public override bool Decide(GameObject go)
    {
        if (go.GetComponent<EnemyHealth>().GetHealthCurrent() <= health)
        {
            return true;
        }
        return false;
    }
}
