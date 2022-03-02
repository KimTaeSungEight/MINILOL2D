using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniLol.Unit
{
    public interface IMovement
    {
        void Move(Vector2 dir);

        void Stop(); 
    }
}