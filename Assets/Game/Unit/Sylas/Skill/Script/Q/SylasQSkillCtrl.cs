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

        private void Progress()
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
            Vector3 mousePos = Input.mousePosition;
            Vector3 createPos = Vector3.zero;
            Ray targetRay = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(targetRay, out RaycastHit result))
            {
                createPos = result.point;
            }

            createPos.z = -0.6f;
            var go = GameObject.Instantiate(_sylasQSkillData.ProjectTileGO, createPos, Quaternion.identity);
            go.GetComponent<ProjectileBase>().Init(SkillDataBase.damage);
        }
    }
}