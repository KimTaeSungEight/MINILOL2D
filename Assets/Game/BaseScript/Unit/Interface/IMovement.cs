using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit
{
    public interface IMovement
    {
        IReactiveProperty<bool> IsMoving { get; }

        void Move(Vector2 dir);

        void Stop(); 
    }
}