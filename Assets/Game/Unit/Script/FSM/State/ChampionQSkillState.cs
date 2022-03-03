using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.FSM
{
    public class ChampionQSkillState : FSMStateBase, MiniInputSystem.IInputEventDisposable
    {
        private IDisposable _inputDisposable;
        public IDisposable InputDisposable => _inputDisposable;

        private Unit.IAnimationCtrl _animationCtrl;

        private Unit.ISkillSlot _skillSlot;

        public override void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            base.InitState(fsmSystem);

            _animationCtrl = _unitModerator.AnimationCtrl;
            _skillSlot = _unitModerator.skillSlotCtrl.GetSkillslot(0);

            if (_unitModerator.IsControllChampion == true)
            {
                InuputSubscribe();
            }
        }

        public override void StartState()
        {
            _animationCtrl.SetAniState(Unit.AnimationEnum.QSkill);
            _skillSlot.Action();
        }

        public override void UpdateState()
        {
        }
        public override void EndState()
        {
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
            _inputDisposable = _unitModerator.InputEventProvider.InputEvent.Where(x => x == TransitionCondition.QSkill)
                .Subscribe(x => FSMSystem.ChangeState(x));
        }

        public void InputDispose()
        {
            _inputDisposable.Dispose();
        }
    }
}