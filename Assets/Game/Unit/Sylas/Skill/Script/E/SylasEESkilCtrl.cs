using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniLol.Unit.Skill
{
    public class SylasEESkilCtrl : SkillCtrlBase
    {
        private SylasEESkillData _sylasEESkillData;
        private AnimatorOverrideController _animatorOverrideController;
        protected AnimationClipOverrides clipOverrides;

        public SylasEESkilCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();

            _sylasEESkillData = SkillDataBase as SylasEESkillData;
            SaveOriginalClips();
            ChangeAnimationClip();
            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            Debug.Log("EE Skill Progerss");
            Release();
        }

        public override void Release()
        {
            base.Release();
            _skillDataBase = null;
            _skillObj = null;
        }

        private void SaveOriginalClips()
        {
            _animatorOverrideController = new AnimatorOverrideController(UnitModerator.AnimationCtrl.Animator.runtimeAnimatorController);
            clipOverrides = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
            _animatorOverrideController.GetOverrides(clipOverrides);
            var clipInfo = UnitModerator.AnimationCtrl.Animator.GetCurrentAnimatorClipInfo(0);
            RuntimeAnimatorController runtimeAnimatorController = UnitModerator.AnimationCtrl.Animator.runtimeAnimatorController;

            clipOverrides["Idle"] = runtimeAnimatorController.animationClips[0];
            clipOverrides["Move"] = runtimeAnimatorController.animationClips[1];
            clipOverrides["QSkill"] = runtimeAnimatorController.animationClips[2];
            clipOverrides["WSkill"] = runtimeAnimatorController.animationClips[3];
            clipOverrides["ESkill"] = runtimeAnimatorController.animationClips[4];
            clipOverrides["RSkill"] = runtimeAnimatorController.animationClips[5];
        }

        private void ChangeAnimationClip()
        {
            UnitModerator.AnimationCtrl.SetAnimations(_animatorOverrideController);
            clipOverrides["ESkill"] = _sylasEESkillData.AnimationClip;
            _animatorOverrideController.ApplyOverrides(clipOverrides);
        }
    }
}