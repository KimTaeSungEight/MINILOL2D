using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit.Skill
{
    public class SkillObj : MonoBehaviour, ISkillObj
    {
        private SkillCtrlBase _controller;
        public SkillCtrlBase Controller => _controller;

        private Subject<UniRx.Unit> _skillEndSubject = new Subject<UniRx.Unit>();
        public IObservable<UniRx.Unit> skillEndObservable => _skillEndSubject;

        public void InitSkill(SkillCtrlBase controller)
        {
            _controller = controller;
            _controller.SkillEndObservable.Subscribe(_ => Release());
            _controller.Invoke();
        }

        public void Release()
        {
            _controller = null;
            _skillEndSubject.OnNext(UniRx.Unit.Default);
            _skillEndSubject.OnCompleted();
            Manager.GameManager.Instance.SkillObjManager.EnqueueSkillObject(this);
        }
    }
}