using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateController : MonoBehaviour
{
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    private void Update()
    {
        DoActions();
        CheckTransitions();
    }

    private void FixedUpdate()
    {
        DoActionsFixed();
    }

    public AIState GetCurrentState()
    {
        return currentState;
    }

    private void changeState(AIState newState)
    {
        if(currentState != newState && newState != remainState)
        {
            currentState = newState;
        }
    }

    private void DoActions()
    {
        foreach(AIAction a in currentState.actions)
        {
            a.Do(this.gameObject);
        }
    }

    private void DoActionsFixed()
    {
        foreach (AIAction a in currentState.actions)
        {
            a.DoFixed(this.gameObject);
        }
    }

    private void CheckTransitions()
    {
        foreach(AITransition t in currentState.transitions)
        {
            if (t.decision.Decide(this.gameObject))
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
