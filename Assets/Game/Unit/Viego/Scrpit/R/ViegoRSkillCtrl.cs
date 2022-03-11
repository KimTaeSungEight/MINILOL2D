using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public class ViegoRSkillCtrl : SkillCtrlBase
    {
        private ViegoRSkillData _viegoRSkillData;
        public ViegoRSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();
            _viegoRSkillData = SkillDataBase as ViegoRSkillData;
            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            MoveLogic();
            AttackLogic();
            Release();
        }

        private void MoveLogic()
        {
            Vector3 mousePos = Manager.GameManager.Instance.GetMousePos();

            UnitModerator.Movement.Rigidbody2D.MovePosition(mousePos);
        }

        private void AttackLogic()
        {
            LayerMask layerMask = LayerMask.GetMask("Hit");

            var hitArr = Physics2D.OverlapBoxAll(UnitModerator.Movement.CurPos, _viegoRSkillData.AttackSize, 0.0f, layerMask);

            foreach (var item in hitArr)
            {
                if (item == null)
                {
                    return;
                }

                item.GetComponent<IDamageApplicable>().ApplyDamage(SkillDataBase.damage);
            }
        }

        public override void Release()
        {
            base.Release();
        }

    }
}