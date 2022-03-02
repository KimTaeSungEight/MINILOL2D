using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.FSM
{
    public abstract class FSMStateBase : MonoBehaviour, IFSMStateBase
    {
        private FSMSystem<TransitionCondition, IFSMStateBase> _fsmSystem = null;
        public FSMSystem<TransitionCondition, IFSMStateBase> FSMSystem => _fsmSystem;

        public virtual void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            this._fsmSystem = fsmSystem;
            _unitModerator = GetComponentInParent<Unit.IUnitModerator>();
        }

        public abstract void StartState();
        public abstract void UpdateState();
        public abstract void EndState();

        public abstract bool Transition(TransitionCondition condition);

        [SerializeField]
        private TransitionCondition condition = TransitionCondition.None;

        public TransitionCondition Condition => condition;

        protected Unit.IUnitModerator _unitModerator;
    }
}