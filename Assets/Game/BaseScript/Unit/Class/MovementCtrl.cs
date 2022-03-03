using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

namespace MiniLol.Unit
{
    public class MovementCtrl : MonoBehaviour, IMovement
    {
        [SerializeField]
        protected Rigidbody2D _rigidbody2D;
        protected float _moveSpeed;
        private BoolReactiveProperty _isMovingProperty = new BoolReactiveProperty(false);

        IReactiveProperty<bool> IMovement.IsMoving => _isMovingProperty;

        private CancellationTokenSource cancellationTokenSource;

        private void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        public virtual void MovementInit(UnitStatBase unitStat)
        {
            _moveSpeed = unitStat.moveSpeed.Value;
            unitStat.moveSpeed.Subscribe(x => _moveSpeed = x);
        }


        public virtual void Move(Vector2 targetPos) 
        {
            if(Vector2.Distance(targetPos, transform.position) < 0.1f)
            {
                return;
            }

            if(_isMovingProperty.Value == true)
            {
                Stop();
            }

            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();
            }

            MoveTask(cancellationTokenSource.Token, targetPos).Forget();
        }

        public virtual void Stop() 
        {
            cancellationTokenSource?.Cancel();
            _isMovingProperty.Value = false;
            cancellationTokenSource = null;
        }

        private async UniTaskVoid MoveTask(CancellationToken ct, Vector2 targetPos)
        {
            _isMovingProperty.Value = true;

            while (!ct.IsCancellationRequested)
            {
                if (Vector2.Distance(targetPos, transform.position) < 0.05f)
                {
                    _isMovingProperty.Value = false;
                    //ct.ThrowIfCancellationRequested();
                    return;
                }

                Vector2 dir = targetPos - new Vector2(transform.position.x, transform.position.y);
                Vector3 movePos = new Vector3(dir.normalized.x, dir.normalized.y, 0.0f) * Time.fixedDeltaTime * _moveSpeed;
                transform.position += movePos; 

                await UniTask.Yield();
            }
        }

        private void OnDestroy()
        {
            Stop();
        }
    }
}