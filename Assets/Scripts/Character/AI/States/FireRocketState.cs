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
        if (fireCount <= 0 || !control.CanShootRocket())
        {
            stateEnded = true;
            return;
        }

        if (control.FireWeaponAtTarget())
        {
            fireCount--;
        }
    }
}
