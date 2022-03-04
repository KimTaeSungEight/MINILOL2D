using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit.Skill
{
    public class ProjectileBase : MonoBehaviour, IProjectile
    {
        private Subject<UniRx.Unit> projectileEndSubject = new Subject<UniRx.Unit>();
        public IObservable<UniRx.Unit> ProjectileEndObservable => projectileEndSubject;

        [SerializeField]
        protected AttackBase _attackBase;

        public void Init(Damage damage)
        {
            _attackBase.Init(damage);
        }

        public virtual void ProjectileEnd()
        {
            projectileEndSubject.OnNext(UniRx.Unit.Default);
            projectileEndSubject.OnCompleted();
            Destroy(this.gameObject);
        }
    }
}