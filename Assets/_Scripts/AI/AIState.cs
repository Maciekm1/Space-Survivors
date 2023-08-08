using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/AIState", fileName = "newAIState")]
public class AIState : ScriptableObject
{
    public List<AIAction> actions;
    public List<AITransition> transitions;
}
