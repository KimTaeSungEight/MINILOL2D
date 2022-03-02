using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MiniLol.FSM
{
    public abstract class FSMSystem<EnumType, State> :  MonoBehaviour where EnumType : System.Enum where State : FSM.IFSMStateBase
    {
        [SerializeField] private EnumType _currState;
        public EnumType CurrState => _currState;

        [SerializeField] private EnumType _startState;
        public EnumType StartState => _startState;

        [SerializeField] private bool _isInit = false;
        public bool IsInit => _isInit;

        private Dictionary<EnumType, State> _fsmData = new Dictionary<EnumType, State>();

        private void Start()
        {
            InitState();
        }

        private void InitState()
        {
            RegisterState();
            ChangeState(StartState);
            _isInit = true;

            this.UpdateAsObservable()
                .Where(_ => _isInit == true && _fsmData.ContainsKey(_currState) == true)
                .Subscribe(_ => _fsmData[CurrState].UpdateState())
                .AddTo(this);
        }

        protected abstract void RegisterState();

        protected void AddState(EnumType type, State fsmState)
        {
            if (fsmState == null)
            {
                Debug.LogError("FSM State Is Null");
                return;
            }

            if (_fsmData.ContainsKey(type) == true)
            {
                Debug.LogError("Have FSMState :" + type.ToString());
                return;
            }

            _fsmData.Add(type, fsmState);
        }

        public void ChangeState(EnumType state)
        {
            if (_fsmData.ContainsKey(state) == false)
                return;

            if (IsInit == true)
            {
                if (_fsmData.ContainsKey(_currState) == true)
                {
                    _fsmData[CurrState].EndState();
                }
            }

            _currState = state;
            _fsmData[state].StartState();
        }

        protected bool GetContainsKey(EnumType type)
        {
            return _fsmData.ContainsKey(type);
        }

        protected State GetState(EnumType type)
        {
            if (_fsmData.ContainsKey(type) == false)
                return default(State);

            return _fsmData[type];
        }
    }
}