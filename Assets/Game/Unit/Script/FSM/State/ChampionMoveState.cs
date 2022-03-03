using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MiniLol.FSM
{
    public class ChampionMoveState : FSMStateBase, MiniInputSystem.IInputEventDisposable
    {
        private IDisposable _inputDisposable;
        private Unit.IAnimationCtrl _animationCtrl;
        private IDisposable _moveEnd;

        public IDisposable InputDisposable => _inputDisposable;

        public override void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            base.InitState(fsmSystem);

            _animationCtrl = _unitModerator.AnimationCtrl;
        }

        public override void StartState()
        {
            if(_unitModerator.IsControllChampion == true)
            {
                InuputSubscribe();
            }

            _animationCtrl.SetAniState(Unit.AnimationEnum.Move);
        }

        public override void UpdateState()
        {
        }

        public override void EndState()
        {
            if (_unitModerator.IsControllChampion == true)
            {
                InputDispose();
            }
        }

        public override bool Transition(TransitionCondition condition)
        {
            return true;
        }


        public void InuputSubscribe()
        {
            //_inputDisposable = _unitModerator.InputEventProvider.InputEvent.Where(x => x == TransitionCondition.None)
            //    .Subscribe(x => FSMSystem.ChangeState(TransitionCondition.Idle));
            _moveEnd = _unitModerator.Movement.IsMoving.Where(x => x == false)
                .Subscribe(_ => FSMSystem.ChangeState(TransitionCondition.Idle));
        }

        public void InputDispose()
        {
            _inputDisposable?.Dispose();
        }

    }
}