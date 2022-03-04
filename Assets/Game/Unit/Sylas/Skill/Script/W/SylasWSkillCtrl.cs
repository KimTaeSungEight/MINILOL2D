using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit.Skill
{
    public class SylasWSkillCtrl : SkillCtrlBase
    {
        private float _orignalMoveSpeed = 0.0f;
        System.IDisposable disposable;
        public SylasWSkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}
        
        public override void Invoke()
        {
            base.Invoke();
            _orignalMoveSpeed = UnitModerator.Stat.unitStat.moveSpeed.Value;
            Progress();
        }

        public override void Progress()
        {
            base.Progress();

            AttackLogic();
            SkillMove();
        }

        public override void Release()
        {
            base.Release();
        }

        private void SkillMove()
        {
            UnitModerator.Stat.unitStat.moveSpeed.Value = _orignalMoveSpeed * 50.0f;

            var mousePos = Manager.GameManager.Instance.GetMousePos();

            UnitModerator.Movement.Move(mousePos);

            disposable = UnitModerator.Movement.IsMoving.Where(x => x == false)
                    .Subscribe(_ => ResetValue());
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

        private void ResetValue()
        {
            Debug.Log("ResetValue");
            UnitModerator.Stat.unitStat.moveSpeed.Value = _orignalMoveSpeed;
            disposable?.Dispose();
            Release();
        }
    }
}