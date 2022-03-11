using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Viego/P", fileName = "ViegoSkillP")]
    public class ViegoPSkillData : SkillDataBase
    {
        [SerializeField]
        private float _duration;
        public float Duration => _duration;

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new ViegoPSkillCtrl(skillObj, this, unitModerator);
        }
    }
}