using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Manager
{
    public class StatDataBank : MonoBehaviour
    {
        [SerializeField]
        List<Unit.UnitStatBase> _unitStatBaseList;

        public Unit.UnitStatBase GetUnitData(int id)
        {
            return _unitStatBaseList.Find(x => x.id == id);
        }

        public Unit.UnitStatBase GetUnitData(string name)
        {
            return new Unit.UnitStatBase(_unitStatBaseList.Find(x => x.name == name));
        }
    }
}