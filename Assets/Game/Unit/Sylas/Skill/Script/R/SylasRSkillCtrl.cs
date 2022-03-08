using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MiniLol.Unit.Skill
{
    public class SylasRSkillCtrl : SkillCtrlBase
    {
        private int _originalRSkillid;
        private int _targetRSkillid;
        private SylasRSkillData _sylasRSkillData;
        private CancellationTokenSource cancellationTokenSource;
        private AnimatorOverrideController _animatorOverrideController;
        protected AnimationClipOverrides clipOverrides;
        System.IDisposable nextDisposable;
        System.IDisposable _nextSkillEndDisposable;

        public SylasRSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();

            _sylasRSkillData = SkillDataBase as SylasRSkillData;
            ChangeOriginalAnimationClip();
            _originalRSkillid = UnitModerator.skillSlotCtrl.GetSkillslot(3).skillDataBase.skillId;
            cancellationTokenSource = new CancellationTokenSource();

            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            if (AttackLogic() == false)
            {
                Release();
                return;
            }

            WaitDuration(cancellationTokenSource.Token).Forget();
        }

        public override void Release()
        {
            base.Release();
            nextDisposable?.Dispose();
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

            Debug.Log(hit.collider.GetComponentInParent<IUnitModerator>().skillSlotCtrl);
            _targetRSkillid = hit.collider.GetComponentInParent<IUnitModerator>().skillSlotCtrl.GetSkillslot(3).skillDataBase.skillId;

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
            clipOverrides["RSkill"] =  Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(_targetRSkillid).AnimationClip;

            UnitModerator.AnimationCtrl.SetAnimations(_animatorOverrideController);
            _animatorOverrideController.ApplyOverrides(clipOverrides);

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
            clipOverrides["RSkill"] = _sylasRSkillData.AnimationClip;

            UnitModerator.AnimationCtrl.SetAnimations(_animatorOverrideController);
            _animatorOverrideController.ApplyOverrides(clipOverrides);
        }

        private void ActiveNextSkill()
        {
            var skillObj = InvokeSkill();
        }

        private void SkillEnd()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }

            _nextSkillEndDisposable?.Dispose();
            Release();
        }

        private ISkillObj InvokeSkill()
        {
            var skillObejct = Manager.GameManager.Instance.SkillObjManager.GetSkillObject();

            if (skillObejct == null)
            {
                return null;
            }

            _nextSkillEndDisposable = skillObejct.skillEndObservable.Subscribe(_ => SkillEnd());
            skillObejct.InitSkill(
                Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(_targetRSkillid).
                GetSkillCtrl(skillObejct, UnitModerator));
            return skillObejct;
        }

        private async UniTaskVoid WaitDuration(CancellationToken ct)
        {
            await UniTask.Delay(300);

            bool isEESkillActive = true;
            nextDisposable = UnitModerator.InputEventProvider.InputEvent.Where(x => x == FSM.TransitionCondition.RSkill)
                .Subscribe(_ =>
                {
                    isEESkillActive = false;
                    ChangeAniamtionClip();
                    ActiveNextSkill();
                });

            await UniTask.Delay(System.TimeSpan.FromSeconds(_sylasRSkillData.Duration), false, PlayerLoopTiming.Update, ct);

            if (isEESkillActive == false)
                return;

            SkillEnd();
        }
    }
}