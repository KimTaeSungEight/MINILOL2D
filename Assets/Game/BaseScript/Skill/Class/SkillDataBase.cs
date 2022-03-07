using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public abstract class SkillDataBase : ScriptableObject
    {
        public int skillId;
        public int skillChampionId;
        public int skillSlotId;
        public int skillDamage;
        public float skillCoolTime;
        public string skillName;
        public string skillDescription;
        public bool isDontCallEnd;
        public Sprite skillSprite;
        public Damage damage;

        [SerializeField]
        private AnimationClip _animationClip;
        public AnimationClip AnimationClip => _animationClip;


        public abstract SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator);
    }
}
