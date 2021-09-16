using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIStateMachine
{
    public interface IState
    {
        float GetDecisionUpdateRate();

        void OnEnter();
        void OnExit();
        void Tick();

        bool StateEnded();
    }
}
