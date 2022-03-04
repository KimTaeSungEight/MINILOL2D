using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public interface IProjectile
    {
        System.IObservable<UniRx.Unit> ProjectileEndObservable { get; }

        void Init(Damage damage);

    }
}