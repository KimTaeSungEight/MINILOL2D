using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public interface IUnitModerator
    {
        IAnimationCtrl AnimationCtrl { get; }
        IMovement Movement { get; }
        
        IStat Stat { get; }

        ISkillSlotCtrl skillSlotCtrl { get; }

        IDamageApplicable DamageApplicable { get; }

        MiniInputSystem.IInputEventProvider InputEventProvider { get; }

        bool IsControllChampion { get; }

        void Init(int championId, bool isControllChampion);
    }
}
