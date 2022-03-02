using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.FSM
{
    public interface IFSMStateBase
    {
        void StartState();

        void UpdateState();

        void EndState();

        bool Transition(TransitionCondition condition);
    }
}