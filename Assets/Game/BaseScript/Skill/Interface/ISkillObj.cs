using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit.Skill
{
    public interface ISkillObj
    {
        SkillCtrlBase Controller { get; }

        void InitSkill(SkillCtrlBase skillCtrlBase);

        void Release();

        System.IObservable<UniRx.Unit> skillEndObservable { get; }
    }
}