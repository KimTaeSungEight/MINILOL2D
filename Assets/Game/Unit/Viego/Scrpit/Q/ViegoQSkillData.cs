using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Viego/Q", fileName = "ViegoSkillQ")]
    public class ViegoQSkillData : SkillDataBase
    {
        [SerializeField]
        private Vector2 _AttackBoxSize;
        public Vector2 AttackBoxSize => _AttackBoxSize;

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new ViegoQSkillCtrl(skillObj, this, unitModerator);
        }
    }
}