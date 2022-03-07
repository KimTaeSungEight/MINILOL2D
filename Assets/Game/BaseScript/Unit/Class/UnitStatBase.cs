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

        public UnitStatBase(UnitStatBase unitStatBase)
        {
            id = unitStatBase.id;
            unitName = unitStatBase.unitName;
            hp = unitStatBase.hp;
            moveSpeed = unitStatBase.moveSpeed;
            mp = unitStatBase.mp;
            addDamage = unitStatBase.addDamage;
            attackInterval = unitStatBase.attackInterval;
            animatorOverrideController = unitStatBase.animatorOverrideController;
        }
    }
}