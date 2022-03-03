using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MiniLol.Unit.Skill;

namespace MiniLol.Unit
{
    public class SkillSlot : ISkillSlot
    {
        private int _slotId;
        public int SlotId => _slotId;

        private FloatReactiveProperty _curCoolTime = new FloatReactiveProperty();
        public IReadOnlyReactiveProperty<float> curCoolTime => _curCoolTime;

        private SkillDataBase _skilldatabase;
        public SkillDataBase skillDataBase => _skilldatabase;

        public SkillSlot(int slotId, int championId)
        {
            _slotId = slotId;
            _skilldatabase = Manager.GameManager.Instance.SkillDataBank.GetSkillDataBase(_slotId, championId);
        }

        public void Action()
        {
            Debug.Log("Action");
        }


    }
}