using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class IdleState : IState
{
    private AIControl control;

    public IdleState(AIControl control)
    {
        this.control = control;
    }

    public float GetDecisionUpdateRate()
    {
        return 0.5f;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public bool StateEnded()
    {
        return false;
    }

    public void Tick()
    {
        control.Idle();
    }
}
