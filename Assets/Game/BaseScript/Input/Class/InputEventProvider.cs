using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using MiniLol.FSM;

namespace MiniLol.MiniInputSystem
{
    public class InputEventProvider : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<Vector2> _moveDirection = new ReactiveProperty<Vector2>();
        private readonly ReactiveProperty<TransitionCondition> _inputEvent = new ReactiveProperty<TransitionCondition>();

        public IReadOnlyReactiveProperty<Vector2> MoveDirection => _moveDirection;

        public IReadOnlyReactiveProperty<TransitionCondition> InputEvent => _inputEvent;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            
            var token = this.GetCancellationTokenOnDestroy();
            InputDetectionLogic(token).Forget();

            _moveDirection.AddTo(this);
            _inputEvent.AddTo(this);

        }

        private async UniTaskVoid InputDetectionLogic(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    var inputPos = Input.mousePosition;
                    Ray targetRay = _mainCamera.ScreenPointToRay(inputPos);
                    if (Physics.Raycast(targetRay, out RaycastHit result))
                    {
                        _moveDirection.SetValueAndForceNotify(result.point);
                    }
                    _inputEvent.Value = TransitionCondition.Move;
                }


                if  (Input.GetKeyDown(KeyCode.Q))
                {
                    _inputEvent.Value = TransitionCondition.QSkill;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    _inputEvent.Value= TransitionCondition.WSkill;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    _inputEvent.Value = TransitionCondition.ESkill;
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    _inputEvent.Value = TransitionCondition.RSkill;
                }

                if (Input.anyKey == false)
                {
                    _inputEvent.Value = TransitionCondition.None;
                }

                await UniTask.Yield();

            }
        }
    }
}