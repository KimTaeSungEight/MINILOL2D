using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{

    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Sylas/R", fileName = "SylasSkillR")]
    public class SylasRSkillData : SkillDataBase
    {
        [SerializeField]
        private float _duration;
        public float Duration => _duration;

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new SylasRSkillCtrl(skillObj, this, unitModerator);
        }
    }
}