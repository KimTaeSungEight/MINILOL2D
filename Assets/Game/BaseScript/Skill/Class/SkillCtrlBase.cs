using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit.Skill
{
    public class SkillCtrlBase : ISkillCtrlBase
    {
        protected ISkillObj _skillObj = null;
        public ISkillObj SkillObj => _skillObj;

        protected SkillDataBase _skillDataBase = null;
        public SkillDataBase SkillDataBase => _skillDataBase;

        private IUnitModerator _unitModerator = null;
        public IUnitModerator UnitModerator => _unitModerator;
       
        protected Subject<UniRx.Unit> _skillEndSubject = new Subject<UniRx.Unit>();

        public System.IObservable<UniRx.Unit> SkillEndObservable => _skillEndSubject;

        public SkillCtrlBase(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator)
        {
            _skillObj = skillObj;
            _skillDataBase = skillDataBase;
            _unitModerator = unitModerator;
        }

        
        public virtual void Invoke(){ }

        public virtual void Progress() { }

        public virtual void Release()
        {
            _skillEndSubject.OnNext(UniRx.Unit.Default);
            _skillEndSubject.OnCompleted();
            _skillDataBase = null;
            _skillObj = null;
        }
    }
}