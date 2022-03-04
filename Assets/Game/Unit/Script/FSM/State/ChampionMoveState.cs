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
        private Unit.IMovement _movement;
        private IDisposable _moveEnd;

        public IDisposable InputDisposable => _inputDisposable;

        public override void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            base.InitState(fsmSystem);

            _animationCtrl = _unitModerator.AnimationCtrl;
            _movement = _unitModerator.Movement;

            if (_unitModerator.IsControllChampion == true)
            {
                InuputSubscribe();
            }
        }

        public override void StartState()
        {
            _animationCtrl.SetAniState(Unit.AnimationEnum.Move);
            _movement.Move(_unitModerator.InputEventProvider.MoveDirection.Value);

            _moveEnd = _unitModerator.Movement.IsMoving.Where(x => x == false)
                        .Subscribe(_ => FSMSystem.ChangeState(TransitionCondition.Idle));

        }

        public override void UpdateState()
        {
        }

        public override void EndState()
        {
            _movement.Stop();
            _moveEnd?.Dispose();
        }

        public override bool Transition(TransitionCondition condition)
        {
            return true;
        }

        private void OnDestroy()
        {
            if (_unitModerator.IsControllChampion == true)
            {
                InputDispose();
            }
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