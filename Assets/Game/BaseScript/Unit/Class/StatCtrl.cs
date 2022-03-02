using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MiniLol.Unit
{
    public class StatCtrl : MonoBehaviour, IStat
    {
        private UnitStatBase _unitStat;

        public IReadOnlyReactiveProperty<float> Hp => _unitStat.hp;

        public IReadOnlyReactiveProperty<float> Mp => _unitStat.mp;

        public AnimatorOverrideController animatorOverride => _unitStat.animatorOverrideController;

        public void Init(int id)
        {
            _unitStat = Manager.GameManager.Instance.StatDataBank.GetUnitData(id);
        }

        public void HandleHp(float changeHp)
        {
            _unitStat.hp.Value += changeHp;
        }

        public void HandleMp(float changeMp)
        {
            _unitStat.mp.Value += changeMp;
        }
    }
}