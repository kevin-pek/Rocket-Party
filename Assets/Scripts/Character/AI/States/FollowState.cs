using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class FollowState : IState
{
    private AIControl control;
    private float stopDistance;

    private bool stateEnded = false;

    public FollowState(AIControl control, float stopDistance)
    {
        this.control = control;
        this.stopDistance = stopDistance;
    }

    public float GetDecisionUpdateRate()
    {
        return 0.2f;
    }

    public void OnEnter()
    {
        stateEnded = false;
    }

    public void OnExit()
    {
        control.Idle();
    }

    public bool StateEnded()
    {
        return stateEnded;
    }

    public void Tick()
    {
        if (stateEnded)
        {
            return;
        }
        stateEnded = control.GoTo(control.GetTargetPos(), stopDistance);
    }
}
