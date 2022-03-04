using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit
{
    public class AnimationCtrl : MonoBehaviour, IAnimationCtrl
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;

        private Subject<UniRx.Unit> _animationEndEvent = new Subject<UniRx.Unit>();
        public IObservable<UniRx.Unit> AnimationEndEvent => _animationEndEvent;

        public int GetCurAni()
        {
            return _animator.GetInteger("State");
        }

        public float GetCurAniTime()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

        }

        public void SetAnimationmTimeScale(float timeScale)
        {
            _animator.speed = timeScale;
        }

        public void SetAnimations(AnimatorOverrideController overrideController)
        {
            _animator.runtimeAnimatorController = overrideController;
        }

        public void SetAniState(AnimationEnum animationEnum)
        {
            _animator.SetInteger("State", (int)animationEnum);
        }

        public void AnimationEnd()
        {
            _animationEndEvent.OnNext(UniRx.Unit.Default);
        }

        private void OnDestroy()
        {
            _animationEndEvent.OnCompleted();
        }
    }
}