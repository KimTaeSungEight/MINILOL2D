using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MiniLol.Unit.Skill;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

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

        private CancellationTokenSource cancellationTokenSource;
        private bool _isCoolTime = false;

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
            if(_isCoolTime == true)
            {
                return;
            }

            _skilldatabase.damage = new Damage(_addDamage + _skilldatabase.skillDamage);
            _skillObj = InvokeSkill();
        }

        private void End()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CoolTimeWaitTask(cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid CoolTimeWaitTask(CancellationToken ct)
        {
            _isCoolTime = true;
            _curCoolTime.Value = 0;
            while (!ct.IsCancellationRequested && _isCoolTime)
            {
                _curCoolTime.Value += Time.fixedDeltaTime;

                if(_curCoolTime.Value >= _skilldatabase.skillCoolTime)
                {
                    _isCoolTime = false;
                    _curCoolTime.Value = _skilldatabase.skillCoolTime;
                }

                await UniTask.WaitForFixedUpdate();
            }
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