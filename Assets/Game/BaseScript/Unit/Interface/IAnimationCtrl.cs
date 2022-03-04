using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public interface IAnimationCtrl
    {
        Animator Animator { get; }

        void SetAniState(AnimationEnum animationEnum);
   
        float GetCurAniTime();

        int GetCurAni();

        void SetAnimationmTimeScale(float timeScale);

        void SetAnimations(AnimatorOverrideController overrideController);

        void AnimationEnd();

        System.IObservable<UniRx.Unit> AnimationEndEvent { get; }
    }
}