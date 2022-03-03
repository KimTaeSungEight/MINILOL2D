using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MiniLol.FSM
{
    public class ChampionFSMSystem : FSMSystem<TransitionCondition, IFSMStateBase>
    {
        private List<FSMStateBase> states = new List<FSMStateBase>();

        protected override void RegisterState()
        {
            states = GetComponents<FSMStateBase>().ToList();

            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].Condition == TransitionCondition.None)
                {
                    Debug.LogError("TransitionCondition is None");
                    return;
                }

                AddState(states[i].Condition, states[i]);
                states[i].InitState(this);
            }
        }

        public override void ChangeState(TransitionCondition state)
        {
            if (state == TransitionCondition.None)
                return;

            if(GetState(CurrState).Transition(state) == false)
            {
                return;
            }

            base.ChangeState(state);
        }
    }
}