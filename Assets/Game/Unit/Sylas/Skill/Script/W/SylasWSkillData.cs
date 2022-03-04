using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Sylas/W", fileName = "SylasSkillW")]

    public class SylasWSkillData : SkillDataBase
    {
        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new SylasWSkillCtrl(skillObj, this, unitModerator);
        }
    }
}