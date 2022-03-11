using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit.Skill
{
    public class SylasWSkillCtrl : SkillCtrlBase
    {
        public SylasWSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}
        
        public override void Invoke()
        {
            base.Invoke();
            Progress();
        }

        public override void Progress()
        {
            base.Progress();

            AttackLogic();
            SkillMove();
            Release();

        }

        public override void Release()
        {
            base.Release();
        }

        private void SkillMove()
        {
            var mousePos = Manager.GameManager.Instance.GetMousePos();

            UnitModerator.Movement.Rigidbody2D.MovePosition(mousePos);


        }

        private void AttackLogic()
        {
            LayerMask layer = LayerMask.GetMask("Hit");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layer);

            if (hit.collider == null)
            {
                return;
            }

            hit.collider.GetComponent<IDamageApplicable>().ApplyDamage(SkillDataBase.damage);

        }
    }
}