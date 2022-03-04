using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.FSM
{
    public class ChampionWSkillState : FSMStateBase, MiniInputSystem.IInputEventDisposable
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
            _skillSlot = _unitModerator.skillSlotCtrl.GetSkillslot(1);  // W

            if (_unitModerator.IsControllChampion == true)
            {
                InuputSubscribe();
            }
        }

        public override void StartState()
        {
            _isSkillEnd = false;
            _animationCtrl.SetAniState(Unit.AnimationEnum.WSkill);
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
            if (_isSkillEnd == false)
            {
                return false;
            }

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
            _inputDisposable = _unitModerator.InputEventProvider.InputEvent.Where(x => x == TransitionCondition.WSkill)
                .Subscribe(x => FSMSystem.ChangeState(x));
        }

        public void InputDispose()
        {
            _inputDisposable?.Dispose();
        }
    }
}