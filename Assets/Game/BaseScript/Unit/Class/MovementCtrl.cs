using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public class MovementCtrl : MonoBehaviour, IMovement
    {
        [SerializeField]
        protected Rigidbody2D _rigidbody2D;
        protected float _moveSpeed;

        public virtual void MovementInit(UnitStatBase unitStat)
        {
            //_moveSpeed = unitStat.moveSpeed.Value;
        }


        public virtual void Move(Vector2 dir) 
        {
            _rigidbody2D.MovePosition(dir);
        }

        public virtual void Stop() { }
    }
}