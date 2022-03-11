using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public class ViegoESkillCtrl : SkillCtrlBase
    {
        private ViegoESkillData _viegoESkillData; 
        public ViegoESkillCtrl(ISkillObj skillObj, SkillDataBase skillDataBase, IUnitModerator unitModerator) : base(skillObj, skillDataBase, unitModerator){}

        public override void Invoke()
        {
            base.Invoke();
            _viegoESkillData = SkillDataBase as ViegoESkillData;
            Progress();
        }

        public override void Progress()
        {
            base.Progress();
            MoveLogic();
            Release();
        }

        private void MoveLogic()
        {
            Vector3 mousePos = Manager.GameManager.Instance.GetMousePos();
            Vector3 dir = mousePos - UnitModerator.Movement.CurPos;
            dir.Normalize();

            UnitModerator.Movement.Rigidbody2D.MovePosition((dir * _viegoESkillData.MovePos) + UnitModerator.Movement.CurPos);
        }

        public override void Release()
        {
            base.Release();
        }
    }
}