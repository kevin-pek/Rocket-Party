using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class WaitState : IState
{
    private AIControl control;

    private bool stateEnded = false;

    private float maxWaitTime;
    private float minWaitTime;
    private float waitTimer;

    public WaitState(AIControl control, float maxWaitTime, float minWaitTime)
    {
        this.control = control;
        this.maxWaitTime = maxWaitTime;
        this.minWaitTime = minWaitTime;
    }

    public float GetDecisionUpdateRate()
    {
        return 0.2f;
    }

    public void OnEnter()
    {
        stateEnded = false;
        waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    public void OnExit()
    {

    }

    public bool StateEnded()
    {
        return stateEnded;
    }

    public void Tick()
    {
        if (waitTimer <= 0)
        {
            stateEnded = true;
        }
        waitTimer -= Time.deltaTime;
        control.Idle();
    }
}
