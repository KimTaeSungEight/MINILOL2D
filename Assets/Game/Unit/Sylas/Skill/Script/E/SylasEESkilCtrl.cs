using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}

namespace MiniLol.Unit.Skill
{
    public class SylasEESkilCtrl : SkillCtrlBase
    {
        private SylasEESkillData _sylasEESkillData;
        private AnimatorOverrideController _animatorOverrideController;
        private AnimationClip _originalClip;

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
        }

        private void SaveOriginalClips()
        {
            _animatorOverrideController = new AnimatorOverrideController(UnitModerator.AnimationCtrl.Animator.runtimeAnimatorController);
            clipOverrides = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
            _animatorOverrideController.GetOverrides(clipOverrides);

            clipOverrides["Idle"] = _animatorOverrideController["Idle"];
            clipOverrides["Move"] = _animatorOverrideController["Move"];
            clipOverrides["QSkill"] = _animatorOverrideController["QSkill"];
            clipOverrides["WSkill"] = _animatorOverrideController["WSkill"];
            clipOverrides["ESkill"] = _animatorOverrideController["ESkill"];
            clipOverrides["RSkill"] = _animatorOverrideController["RSkill"];
        }

        private void ChangeAnimationClip()
        {
            UnitModerator.AnimationCtrl.SetAnimations(_animatorOverrideController);
            clipOverrides["ESkill"] = _sylasEESkillData.AnimationClip;
            _animatorOverrideController.ApplyOverrides(clipOverrides);
        }
    }
}