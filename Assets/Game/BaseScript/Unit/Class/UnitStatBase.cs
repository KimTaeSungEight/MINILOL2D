using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/UnitStat", fileName = "UnitStat")]
    public class UnitStatBase : ScriptableObject
    {
        public int id;
        public string unitName;
        public FloatReactiveProperty hp;
        public FloatReactiveProperty moveSpeed;
        public FloatReactiveProperty mp;
        public FloatReactiveProperty addDamage;
        public float attackInterval;

        public AnimatorOverrideController animatorOverrideController;
    }
}