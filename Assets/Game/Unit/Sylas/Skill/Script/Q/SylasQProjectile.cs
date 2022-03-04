using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit.Skill
{
    public class SylasQProjectile : ProjectileBase
    {
        public override void ProjectileEnd()
        {
            _attackBase.Progress();
            base.ProjectileEnd();
        }
    }
}