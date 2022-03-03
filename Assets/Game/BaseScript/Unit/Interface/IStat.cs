using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit
{
    public interface IStat
    {
        void HandleHp(float changeHp);
        void Init(int id);

        IReadOnlyReactiveProperty<float> Hp { get; }
        IReadOnlyReactiveProperty<float> Mp { get; }

        AnimatorOverrideController animatorOverride { get; }

        UnitStatBase unitStat { get; }
    }
}