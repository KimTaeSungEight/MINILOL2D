using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.FSM
{
    public class ChampionIdleState : FSMStateBase, MiniInputSystem.IInputEventDisposable
    {
        private Unit.IAnimationCtrl _animationCtrl;
        private IDisposable _inputDisposable;

        public IDisposable InputDisposable => _inputDisposable;

        public override void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            base.InitState(fsmSystem);

            _animationCtrl = _unitModerator.AnimationCtrl;
        }

        public override void StartState()
        {
            if (_unitModerator.IsControllChampion == true)
            {
                InuputSubscribe();
            }
            _animationCtrl.SetAniState(Unit.AnimationEnum.Idle);
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
            _inputDisposable = _unitModerator.InputEventProvider.InputEvent.Where(x => x == TransitionCondition.Move)
            .Subscribe(x => FSMSystem.ChangeState(x));
        }

        public void InputDispose()
        {
            _inputDisposable?.Dispose();
        }
    }
}