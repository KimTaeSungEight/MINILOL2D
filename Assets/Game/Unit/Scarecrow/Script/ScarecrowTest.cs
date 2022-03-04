using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public class ScarecrowTest : MonoBehaviour, IDamageApplicable
    {
        public void ApplyDamage(Damage damage)
        {
            Debug.Log("Hit! Damage : " + damage.AttackDamage);
        }
    }
}