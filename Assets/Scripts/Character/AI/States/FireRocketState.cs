using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class FireRocketState : IState
{
    private int maxFireNum = 4;
    private int minFireNum = 1;

    private AIControl control;

    private bool stateEnded = false;
    private int fireCount;

    public FireRocketState(AIControl control)
    {
        this.control = control;
    }

    public float GetDecisionUpdateRate()
    {
        return 0.5f;
    }

    public void OnEnter()
    {
        stateEnded = false;
        fireCount = Random.Range(minFireNum, maxFireNum);
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
        if (fireCount <= 0)
        {
            stateEnded = true;
            return;
        }

        if (control.FireRocket(GetFireAngle()))
        {
            fireCount--;
        }
    }
    
    Vector3 GetFireAngle()
    {
        Vector3 targetPos = control.GetTargetPos();
        Vector3 myPos = control.GetPosition();
        Vector3 diff = targetPos - myPos;
        float angle = Mathf.Atan2(diff.y, diff.x);
        return new Vector3(0f, 0f, Mathf.Rad2Deg * angle - 90);
    }
}
