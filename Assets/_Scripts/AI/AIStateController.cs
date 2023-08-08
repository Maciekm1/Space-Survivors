using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateController : MonoBehaviour
{
    [SerializeField] private AIState currentState;

    private void Update()
    {
        DoActions();
        CheckTransitions();
    }

    private void FixedUpdate()
    {
        DoActionsFixed();
    }

    private void changeState(AIState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

    private void DoActions()
    {
        foreach(AIAction a in currentState.actions)
        {
            a.Do(this);
        }
    }

    private void DoActionsFixed()
    {
        foreach (AIAction a in currentState.actions)
        {
            a.DoFixed(this);
        }
    }

    private void CheckTransitions()
    {
        foreach(AITransition t in currentState.transitions)
        {
            if (t.decision.Decide(this))
            {
                changeState(t.trueState);
            }
            else
            {
                changeState(t.falseState);
            }
        }
    }
}
