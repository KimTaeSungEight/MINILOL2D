using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    [CreateAssetMenu(menuName = "ScriptableObject/Skill/Sylas", fileName = "SylasSkill")]
    public class SylasQSkillData : SkillDataBase
    {
        [SerializeField]
        private GameObject _projecttileGO;
        public GameObject ProjectTileGO => _projecttileGO;

        public override SkillCtrlBase GetSkillCtrl(SkillObj skillObj, IUnitModerator unitModerator)
        {
            return new SylasQSkillCtrl(skillObj, this, unitModerator);
        }
    }
}