using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Viego/E", fileName = "ViegoSkillE")]
    public class ViegoESkillData : SkillDataBase
    {
        [SerializeField]
        private float _movePos;
        public float MovePos => _movePos;
        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new ViegoESkillCtrl(skillObj, this, unitModerator);
        }
    }
}