using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
namespace MiniLol.Unit.Skill
{
    public class ViegoPSkillCtrl : SkillCtrlBase
    {
        private ViegoPSkillData _viegoPSkillData;
        private int _originalChampionId;
        private int _targetChampionId;
        private CancellationTokenSource cancellationTokenSource;
        private AnimatorOverrideController _animatorOverrideController;
        private System.IDisposable _inputRKeyDisposable;

        protected AnimationClipOverrides clipOverrides;

        public ViegoPSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();
            _originalChampionId = UnitModerator.Stat.unitStat.id;
            _viegoPSkillData = SkillDataBase as ViegoPSkillData;
            cancellationTokenSource = new CancellationTokenSource();
            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            if(AttackLogic() == false)
            {
                Release();
                return;
            }

            ChangeTargetChampion();
            WaitDuration(cancellationTokenSource.Token).Forget();
        }

        private bool AttackLogic()
        {
            LayerMask layer = LayerMask.GetMask("Hit");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layer);

            if (hit.collider == null)
            {
                return false;
            }

            _targetChampionId = hit.collider.GetComponentInParent<IUnitModerator>().Stat.unitStat.id;

            return true;

        }

        private void ChangeAniamtionClip()
        {
            _animatorOverrideController = new AnimatorOverrideController(UnitModerator.AnimationCtrl.Animator.runtimeAnimatorController);
            clipOverrides = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
            _animatorOverrideController.GetOverrides(clipOverrides);

            RuntimeAnimatorController runtimeAnimatorController = UnitModerator.AnimationCtrl.Animator.runtimeAnimatorController;

            clipOverrides["Idle"] = runtimeAnimatorController.animationClips[0];
            clipOverrides["Move"] = runtimeAnimatorController.animationClips[1];
            clipOverrides["QSkill"] = runtimeAnimatorController.animationClips[2];
            clipOverrides["WSkill"] = runtimeAnimatorController.animationClips[3];
            clipOverrides["ESkill"] = runtimeAnimatorController.animationClips[4];
            clipOverrides["RSkill"] = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(3, _originalChampionId).AnimationClip;

            UnitModerator.AnimationCtrl.SetAnimations(_animatorOverrideController);
            _animatorOverrideController.ApplyOverrides(clipOverrides);

        }

        private void ChangeTargetChampion()
        {
            UnitModerator.Stat.Init(_targetChampionId);
            UnitModerator.Movement.Init(UnitModerator.Stat.unitStat);
            UnitModerator.AnimationCtrl.SetAnimations(UnitModerator.Stat.animatorOverride);

            for (int i = 0; i < 3; i++)
            {
                UnitModerator.skillSlotCtrl.GetSkillslot(i).InsertSkillData(Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(i, _targetChampionId).skillId);
            }

            //UnitModerator.skillSlotCtrl.GetSkillslot(3).InsertSkillData(Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(3, _originalChampionId).skillId);
            ChangeAniamtionClip();
        }

        private void ChangeOriginalChampion()
        {
            UnitModerator.Stat.Init(_originalChampionId);
            UnitModerator.Movement.Init(UnitModerator.Stat.unitStat);
            UnitModerator.AnimationCtrl.SetAnimations(UnitModerator.Stat.animatorOverride);
            for (int i = 0; i < 3; i++)
            {
                UnitModerator.skillSlotCtrl.GetSkillslot(i).InsertSkillData(Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(i, _originalChampionId).skillId);
            }

            Release();
        }

        private async UniTaskVoid WaitDuration(CancellationToken ct)
        {
            await UniTask.Delay(300);

            bool isRSkillNotPress = true;
            _inputRKeyDisposable = UnitModerator.InputEventProvider.InputEvent.Where(x => x == FSM.TransitionCondition.RSkill)
                .Subscribe(_ =>
                {
                    isRSkillNotPress = false;
                    ChangeOriginalChampion();
                });
            await UniTask.Delay(System.TimeSpan.FromSeconds(_viegoPSkillData.Duration), false, PlayerLoopTiming.Update, ct);

            if (isRSkillNotPress == true)
            {
                ChangeOriginalChampion();
            }
        }

        public override void Release()
        {
            _inputRKeyDisposable?.Dispose();
            base.Release();
        }
    }
}