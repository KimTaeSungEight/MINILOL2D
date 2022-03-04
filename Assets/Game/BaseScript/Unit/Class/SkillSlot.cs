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
        private int _championId;

        private float _addDamage;

        private IUnitModerator _unitModerator;

        private FloatReactiveProperty _curCoolTime = new FloatReactiveProperty();
        public IReadOnlyReactiveProperty<float> curCoolTime => _curCoolTime;

        private SkillDataBase _skilldatabase;
        public SkillDataBase skillDataBase => _skilldatabase;

        private ISkillObj _skillObj;

        public SkillSlot(int slotId, int championId, IUnitModerator unitModerator)
        {
            _slotId = slotId;
            _championId = championId;
            _skilldatabase = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(_slotId, _championId);
            _unitModerator = unitModerator;

            _unitModerator.Stat.unitStat.addDamage.Subscribe(x => _addDamage = x);
        }

        public void Action()
        {
            _skilldatabase.damage = new Damage(_addDamage + _skilldatabase.skillDamage);
            _skillObj = InvokeSkill();
            Debug.Log("Action");
        }

        private void End()
        {

        }

        public void ChangeAddDamage(float addDamage)
        {
            _addDamage = addDamage;
        }

        private ISkillObj InvokeSkill()
        {
            var skillObejct = Manager.GameManager.Instance.SkillObjManager.GetSkillObject();

            if (skillObejct == null)
            {
                Debug.LogError("SkillObj is Null");
                return null;
            }

            skillObejct.skillEndObservable.Subscribe(_ => End());
            skillObejct.InitSkill(_skilldatabase.GetSkillCtrl(skillObejct, _unitModerator));
            return skillObejct;
        }

        public void InsertSkillData(int skillId)
        {
            _skilldatabase = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(skillId);
        }
    }
}