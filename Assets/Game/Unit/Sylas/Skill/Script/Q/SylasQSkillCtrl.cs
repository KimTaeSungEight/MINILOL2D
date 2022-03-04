using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public class SylasQSkillCtrl : SkillCtrlBase
    {
        private SylasQSkillData _sylasQSkillData;

        public SylasQSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();

            _sylasQSkillData = SkillDataBase as SylasQSkillData;

            Progress();
        }

        public override void Progress()
        {
            Debug.Log("Skill Progress");
            CreateProjectTile();
            Release();
        }

        public override void Release()
        {
            base.Release();
        }

        private void CreateProjectTile()
        {
            var mousePos = Manager.GameManager.Instance.GetMousePos();

            var go = GameObject.Instantiate(_sylasQSkillData.ProjectTileGO, mousePos, Quaternion.identity);
            go.GetComponent<ProjectileBase>().Init(SkillDataBase.damage);
        }
    }
}