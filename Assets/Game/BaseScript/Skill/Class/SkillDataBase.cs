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
        public string skillName;
        public string skillDescription;
        public Sprite skillSprite;
        public Damage damage;

        public abstract SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator);
    }
}
