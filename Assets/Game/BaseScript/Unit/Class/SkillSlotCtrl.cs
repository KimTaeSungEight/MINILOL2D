using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public class SkillSlotCtrl : MonoBehaviour, ISkillSlotCtrl
    {
        private List<SkillSlot> _skillSlots = new List<SkillSlot>();

        public void Init(int championId, IUnitModerator unitModerator)
        {
            for (int i = 0; i < 4; i++) // ��ų ������ 4���� ���� ������ �����Ƿ� �׳� ����.
            {
                _skillSlots.Add(new SkillSlot(i, championId, unitModerator));
            }
        }

        public ISkillSlot GetSkillslot(int skillSlotId)
        {
            return _skillSlots.Find(x => x.SlotId == skillSlotId);
        }
    }
}