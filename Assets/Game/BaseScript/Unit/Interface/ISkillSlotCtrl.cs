using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public interface ISkillSlotCtrl
    {
        void Init(int championId, IUnitModerator unitModerator);

        ISkillSlot GetSkillslot(int skillSlotId);
    }
}