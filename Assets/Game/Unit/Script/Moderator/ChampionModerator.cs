using MiniLol.MiniInputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MiniLol.Unit
{
    public class ChampionModerator : MonoBehaviour, IUnitModerator
    {
        // todo ���߿� private���� �ٲ㼭 ��Ʈ�� �ؾ� �� ��.
        private bool _isControllChampion = false;
        public bool IsControllChampion => _isControllChampion;
        private int _championId = 0;

        private AnimationCtrl _animationCtrl;
        private MovementCtrl _movementCtrl;
        private StatCtrl _statCtrl;
        private InputEventProvider _inputEventProvider;
        private SkillSlotCtrl _skillSlotCtrl;

        public IAnimationCtrl AnimationCtrl => _animationCtrl;

        public IMovement Movement => _movementCtrl;

        public IStat Stat => _statCtrl;

        public IDamageApplicable DamageApplicable => throw new System.NotImplementedException();

        public IInputEventProvider InputEventProvider
        {
            get
            {
                if (IsControllChampion == false)
                {
                    Debug.LogError("Is Not Controll Champion!");
                    return null;
                }
                else
                {
                    return _inputEventProvider;
                }

            }
        }

        bool IUnitModerator.IsControllChampion => IsControllChampion;

        public ISkillSlotCtrl skillSlotCtrl => _skillSlotCtrl;

        public void Init(int championId, bool isControllChampion)
        {
            _isControllChampion = isControllChampion;
            _championId = championId;

            if (IsControllChampion == true)
            {
                _inputEventProvider = gameObject.AddComponent<InputEventProvider>();
            }

            _animationCtrl = GetComponent<AnimationCtrl>();
            _movementCtrl = GetComponent<MovementCtrl>();
            _statCtrl = GetComponent<StatCtrl>();
            _skillSlotCtrl = GetComponent<SkillSlotCtrl>();

            _statCtrl.Init(_championId);
            _movementCtrl.MovementInit(_statCtrl.unitStat);

            _inputEventProvider?.MoveDirection.Subscribe(x => _movementCtrl.Move(x));

            _animationCtrl.SetAnimations(_statCtrl.animatorOverride);
            _skillSlotCtrl.Init(_championId, this);
        }
    }
}