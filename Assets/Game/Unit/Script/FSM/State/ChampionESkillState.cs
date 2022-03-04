using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.FSM
{
    public class ChampionESkillState : FSMStateBase, MiniInputSystem.IInputEventDisposable
    {
        private IDisposable _inputDisposable;
        public IDisposable InputDisposable => _inputDisposable;

        private Unit.IAnimationCtrl _animationCtrl;
        private Unit.ISkillSlot _skillSlot;
        private bool _isSkillEnd = false;

        public override void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            base.InitState(fsmSystem);

            _animationCtrl = _unitModerator.AnimationCtrl;
            _animationCtrl.AnimationEndEvent.Subscribe(_ => {
                _isSkillEnd = true;
                FSMSystem.ChangeState(TransitionCondition.Idle);
            })
                                      .AddTo(gameObject);
            _skillSlot = _unitModerator.skillSlotCtrl.GetSkillslot(2);  // E

            if (_unitModerator.IsControllChampion == true)
            {
                InuputSubscribe();
            }
        }

        public override void StartState()
        {
            _isSkillEnd = false;
            _skillSlot.Action();
            _animationCtrl.SetAniState(Unit.AnimationEnum.ESkill);
        }

        public override void UpdateState()
        {
        }
        public override void EndState()
        {
        }

        public override bool Transition(TransitionCondition condition)
        {
            if (_isSkillEnd == false)
            {
                return false;
            }

            return true;
        }
        public void InuputSubscribe()
        {
            _inputDisposable = _unitModerator.InputEventProvider.InputEvent.Where(x => x == TransitionCondition.ESkill)
                .Subscribe(x => FSMSystem.ChangeState(x));
        }

        public void InputDispose()
        {
            _inputDisposable?.Dispose();
        }
    }
}