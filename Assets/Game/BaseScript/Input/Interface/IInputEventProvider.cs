using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.MiniInputSystem
{
    public interface IInputEventProvider
    {
        IReadOnlyReactiveProperty<Vector2> MoveDirection { get; }
        IReadOnlyReactiveProperty<FSM.TransitionCondition> InputEvent { get; }

    }
}