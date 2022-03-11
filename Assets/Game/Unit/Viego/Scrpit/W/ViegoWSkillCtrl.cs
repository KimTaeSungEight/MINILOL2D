using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MiniLol.Unit.Skill
{
    public class ViegoWSkillCtrl : SkillCtrlBase
    {
        private ViegoWSkillData _viegoWSkillData;
        private CancellationTokenSource _cancellationTokenSource;

        public ViegoWSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();
            _cancellationTokenSource = new CancellationTokenSource();
            _viegoWSkillData = SkillDataBase as ViegoWSkillData;

            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            WaitDuration(_cancellationTokenSource.Token).Forget();
            Release();

        }

        private async UniTaskVoid WaitDuration(CancellationToken ct)
        {
            UnitModerator.Stat.unitStat.moveSpeed.Value += _viegoWSkillData.AddMoveSpeed;

            await UniTask.Delay(System.TimeSpan.FromSeconds(_viegoWSkillData.Duration));

            UnitModerator.Stat.unitStat.moveSpeed.Value -= _viegoWSkillData.AddMoveSpeed;
        }

        public override void Release()
        {
            base.Release();
        }
    }
}