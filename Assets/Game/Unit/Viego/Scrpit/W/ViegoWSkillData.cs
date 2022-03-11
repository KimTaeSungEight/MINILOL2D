using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Viego/W", fileName = "ViegoSkillW")]
    public class ViegoWSkillData : SkillDataBase
    {
        [SerializeField]
        private float _addMoveSpeed;
        public float AddMoveSpeed => _addMoveSpeed;
        [SerializeField]
        private float _duration;
        public float Duration => _duration;

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new ViegoWSkillCtrl(skillObj, this, unitModerator);
        }
    }
}