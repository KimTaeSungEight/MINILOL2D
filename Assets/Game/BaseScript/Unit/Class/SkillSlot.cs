using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MiniLol.Unit.Skill;
using System;

namespace MiniLol.Unit
{
    public class SkillSlot : ISkillSlot
    {
        private int _slotId;
        public int SlotId => _slotId;

        private float _addDamage;

        private FloatReactiveProperty _curCoolTime = new FloatReactiveProperty();
        public IReadOnlyReactiveProperty<float> curCoolTime => _curCoolTime;

        private SkillDataBase _skilldatabase;
        public SkillDataBase skillDataBase => _skilldatabase;

        private Subject<UniRx.Unit> _skillEndSubJect = new Subject<UniRx.Unit>();
        public IObservable<UniRx.Unit> skillEndSubject => _skillEndSubJect;

        public SkillSlot(int slotId, int championId)
        {
            _slotId = slotId;
            _skilldatabase = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(_slotId, championId);
        }

        public void Action()
        {
            Damage damage = new Damage(_addDamage + _skilldatabase.skillDamage);

            Debug.Log("Action");

            _skillEndSubJect.OnNext(UniRx.Unit.Default);
        }

        public void ChangeAddDamage(float addDamage)
        {
            _addDamage = addDamage;
        }
    }
}