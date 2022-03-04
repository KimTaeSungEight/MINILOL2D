using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public interface ISkillCtrlBase
    {
        ISkillObj SkillObj { get; }

        SkillDataBase SkillDataBase { get; }

        IUnitModerator UnitModerator { get; }

        System.IObservable<UniRx.Unit> SkillEndObservable { get; }
    }
}