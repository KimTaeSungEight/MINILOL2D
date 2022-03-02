using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public interface IDamageApplicable
    {
        void ApplyDamage(Damage damage);
    }

}