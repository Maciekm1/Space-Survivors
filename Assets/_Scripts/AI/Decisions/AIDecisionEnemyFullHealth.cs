using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIDecision/EnemyFullHealth")]
public class AIDecisionEnemyFullHealth : AIDecision
{
    public override bool Decide(GameObject go)
    {
        if (go.GetComponent<EnemyHealth>().GetHealthCurrent() == go.GetComponent<EnemyHealth>().GetHealthMax())
        {
            return true;
        }
        return false;
    }
}
