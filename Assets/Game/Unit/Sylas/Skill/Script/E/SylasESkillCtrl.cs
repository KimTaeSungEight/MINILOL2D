using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MiniLol.Unit.Skill
{
    public class SylasESkillCtrl : SkillCtrlBase
    {
        SylasESkillData _sylasESkillData;
        private float _orignalMoveSpeed = 0.0f;
        System.IDisposable disposable;
        System.IDisposable nextDisposable;
        private CancellationTokenSource cancellationTokenSource;
        private AnimatorOverrideController _animatorOverrideController;
        protected AnimationClipOverrides clipOverrides;

        public SylasESkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {

            base.Invoke();
            _sylasESkillData = SkillDataBase as SylasESkillData;
            ChangeOriginalAnimationClip();
            cancellationTokenSource = new CancellationTokenSource();
            _orignalMoveSpeed = UnitModerator.Stat.unitStat.moveSpeed.Value;
            Progress();
        }

        public override void Progress()
        {
            base.Progress();

            SkillMove();
            InsertNextSkillData();
            WaitDuration(cancellationTokenSource.Token).Forget();
        }

        public override void Release()
        {
            base.Release();
            nextDisposable?.Dispose();

            Debug.Log("SkillE Release");
        }

        private void ChangeOriginalAnimationClip()
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
            clipOverrides["RSkill"] = runtimeAnimatorController.animationClips[5];

            UnitModerator.AnimationCtrl.SetAnimations(_animatorOverrideController);
            clipOverrides["ESkill"] = _sylasESkillData.AnimationClip;
            _animatorOverrideController.ApplyOverrides(clipOverrides);

        }

        private void InsertNextSkillData()
        {
            UnitModerator.skillSlotCtrl.GetSkillslot(2).InsertSkillData(_sylasESkillData.NextSkillDataId);
        }

        private void InsertOriginalSkillData()
        {
            UnitModerator.skillSlotCtrl.GetSkillslot(2).InsertSkillData(_sylasESkillData.skillId);
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            Release();
        }

        private async UniTaskVoid WaitDuration(CancellationToken ct)
        {
            await UniTask.Delay(300);

            bool isEESkillActive = true;
            nextDisposable = UnitModerator.InputEventProvider.InputEvent.Where(x => x == FSM.TransitionCondition.ESkill)
                .Subscribe(_ => 
                { 
                    isEESkillActive = false;
                    InsertOriginalSkillData();
                });

            await UniTask.Delay(System.TimeSpan.FromSeconds(_sylasESkillData.NextSkillDuration), false, PlayerLoopTiming.Update, ct);

            if (isEESkillActive == false)
                return;

            InsertOriginalSkillData();
        }

        private void SkillMove()
        {
            UnitModerator.Stat.unitStat.moveSpeed.Value = _orignalMoveSpeed * 50.0f;

            var mousePos = Manager.GameManager.Instance.GetMousePos();

            UnitModerator.Movement.Move(mousePos);

            disposable = UnitModerator.Movement.IsMoving.Where(x => x == false)
                    .Subscribe(_ => ResetValue());
        }

        private void ResetValue()
        {
            Debug.Log("ResetValue");
            UnitModerator.Stat.unitStat.moveSpeed.Value = _orignalMoveSpeed;
            disposable?.Dispose();
        }
    }
}