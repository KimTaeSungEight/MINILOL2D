using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Sylas/E", fileName = "SylasSkillE")]
    public class SylasESkillData : SkillDataBase
    {
        [SerializeField]
        private int _nextSkillDataId;
        public int NextSkillDataId => _nextSkillDataId;
        [SerializeField]
        private float _nextSkillDuration;
        public float NextSkillDuration => _nextSkillDuration; 

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new SylasESkillCtrl(skillObj, this, unitModerator);
        }
    }
}