using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.FSM
{
    public class ChampionIdleState : FSMStateBase
    {
        private Unit.IAnimationCtrl _animationCtrl;

        public override void InitState(FSMSystem<TransitionCondition, IFSMStateBase> fsmSystem)
        {
            base.InitState(fsmSystem);

            _animationCtrl = _unitModerator.AnimationCtrl;
        }

        public override void StartState()
        {
            _animationCtrl.SetAniState(Unit.AnimationEnum.Idle);
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
    }
}