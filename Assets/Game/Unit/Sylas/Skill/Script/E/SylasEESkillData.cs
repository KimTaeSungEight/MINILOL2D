using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Sylas/EE", fileName = "SylasSkillEE")]
    public class SylasEESkillData : SkillDataBase
    {
        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new SylasEESkilCtrl(skillObj, this, unitModerator);
        }
    }
}