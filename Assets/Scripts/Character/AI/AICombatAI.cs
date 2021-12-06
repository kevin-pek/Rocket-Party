using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class AICombatAI : MonoBehaviour
{
    public float startFollowDistance;
    public float stopDistance;

    private AIControl control;
    private StateMachine stateMachine;

    private void Awake()
    {
        control = GetComponent<AIControl>();
        stateMachine = new StateMachine();
    }

    private void Start()
    {
        // IState idleState = new IdleState(control);
        IState patrolState = new PatrolState(control);
        IState followState = new FollowState(control, stopDistance);
        IState fireRocketState = new FireRocketState(control);
        IState waitState = new WaitState(control, 0.5f, 1.2f);

        // idle
        stateMachine.AddTransition(patrolState, followState, () => control.GetTargetDistance() < startFollowDistance);

        // follow
        stateMachine.AddTransition(followState, fireRocketState, CurrentStatedEnded);

        // fire rocket
        stateMachine.AddTransition(fireRocketState, waitState, CurrentStatedEnded);

        // wait
        stateMachine.AddTransition(waitState, patrolState, CurrentStatedEnded);

        stateMachine.SetState(patrolState);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private bool CurrentStatedEnded()
    {
        return stateMachine.CurrentState.StateEnded();
    }
}
