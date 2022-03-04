using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace MiniLol.Unit
{
    public interface ISkillSlot
    {
        int SlotId { get; }
        IReadOnlyReactiveProperty<float> curCoolTime { get; }
        
        Skill.SkillDataBase skillDataBase { get; }

        void Action();

        void ChangeAddDamage(float addDamage);
    }
}