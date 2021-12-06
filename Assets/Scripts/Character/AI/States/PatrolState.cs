using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class PatrolState : IState
{
    private AIControl control;

    public PatrolState(AIControl control)
    {
        this.control = control;
    }

    public float GetDecisionUpdateRate()
    {
        return 0.2f;
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
        control.Patrol();
    }
}
