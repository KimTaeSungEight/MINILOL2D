using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Manager
{
    public class SkillDataBank : MonoBehaviour
    {
        [SerializeField]
        private List<Unit.Skill.SkillDataBase> _skillDataBases;

        public Unit.Skill.SkillDataBase GetSkillDataBase(int slotId, int championId)
        {
            return _skillDataBases.Find(x => x.skillChampionId == championId && x.skillSlotId == slotId);
        }
    }
}