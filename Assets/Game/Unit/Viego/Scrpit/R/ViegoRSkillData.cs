using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Viego/R", fileName = "ViegoSkillR")]
    public class ViegoRSkillData : SkillDataBase
    {
        [SerializeField]
        private Vector2 _attackSize;
        public Vector2 AttackSize => _attackSize;

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new ViegoRSkillCtrl(skillObj, this, unitModerator);
        }
    }
}