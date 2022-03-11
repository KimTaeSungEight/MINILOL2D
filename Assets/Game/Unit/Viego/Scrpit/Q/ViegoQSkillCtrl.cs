using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public class ViegoQSkillCtrl : SkillCtrlBase
    {
        private ViegoQSkillData _viegoQSkillData;
        public ViegoQSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();
            _viegoQSkillData = SkillDataBase as ViegoQSkillData;
            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            AttackLogic();
            Release();
        }

        private void AttackLogic()
        {
            LayerMask layerMask = LayerMask.GetMask("Hit");
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mouse.y - UnitModerator.Movement.CurPos.y, mouse.x - UnitModerator.Movement.CurPos.x) * Mathf.Rad2Deg;
            var hitArr = Physics2D.OverlapBoxAll(UnitModerator.Movement.CurPos, _viegoQSkillData.AttackBoxSize, angle, layerMask);
            
            foreach (var item in hitArr)
            {
                if(item == null)
                    continue;

                item.GetComponent<IDamageApplicable>().ApplyDamage(SkillDataBase.damage);
            }
        }

        public override void Release()
        {
            base.Release();
        }
    }
}