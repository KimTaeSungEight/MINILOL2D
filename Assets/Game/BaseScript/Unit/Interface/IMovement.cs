using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit
{
    public interface IMovement
    {
        IReactiveProperty<bool> IsMoving { get; }

        void Init(UnitStatBase unitStat);

        void Move(Vector2 dir);

        void Stop(); 

        Vector3 CurPos { get; }

        Rigidbody2D Rigidbody2D { get; }
    }
}