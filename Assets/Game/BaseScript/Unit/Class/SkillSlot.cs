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
        private IDisposable _skillEndDisposable;

        private CancellationTokenSource cancellationTokenSource;
        private bool _isCoolTime = false;

        private bool isCurActiveSkill = false;
        public bool _isCurActiveSkill
        {
            get { return isCurActiveSkill; }
            set {
                Debug.Log("CallIsCurActiveSkill : " + value);
                isCurActiveSkill = value; }
        } 

        public SkillSlot(int slotId, int championId, IUnitModerator unitModerator)
        {
            _slotId = slotId;
            _championId = championId;
            _skilldatabase = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(_slotId, _championId);
            _unitModerator = unitModerator;
            _curCoolTime.Value = 0;
            _unitModerator.Stat.unitStat.addDamage.Subscribe(x => _addDamage = x);
        }

        public void Action()
        {
            if(_isCoolTime == true)
            {
                return;
            }

            _skilldatabase.damage = new Damage(_addDamage + _skilldatabase.skillDamage);
            Debug.Log("Action : " + _isCurActiveSkill);
            _skillObj = InvokeSkill();
            _isCurActiveSkill = true;
        }

        private void End()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CoolTimeWaitTask(cancellationTokenSource.Token).Forget();
            _skillEndDisposable?.Dispose();
            Debug.Log("SkillSlot End : " + _isCurActiveSkill);
        }

        private async UniTaskVoid CoolTimeWaitTask(CancellationToken ct)
        {
            _isCoolTime = true;
            _curCoolTime.Value = _skilldatabase.skillCoolTime;

            while (!ct.IsCancellationRequested && _isCoolTime)
            {
                _curCoolTime.Value -= Time.fixedDeltaTime;

                if(_curCoolTime.Value <= 0.0f)
                {
                    _isCoolTime = false;
                    _curCoolTime.Value = 0;
                }

                await UniTask.WaitForFixedUpdate();
            }

            _isCurActiveSkill = false;  // _isCurActiveSkill 를 이곳에 빼는 이유.
                                        // Action함수가 끝나기도 전에 End()함수가 호출 되는 경우가 종종 있음.
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
            Debug.Log("_isCurActiveSkill : " + _isCurActiveSkill);

            if (_isCurActiveSkill == false)
            {
                _skillEndDisposable = skillObejct.skillEndObservable.Subscribe(_ => End());
            }
            skillObejct.InitSkill(_skilldatabase.GetSkillCtrl(skillObejct, _unitModerator));
            return skillObejct;
        }

        public void InsertSkillData(int skillId)
        {
            _skilldatabase = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(skillId);
        }
    }
}