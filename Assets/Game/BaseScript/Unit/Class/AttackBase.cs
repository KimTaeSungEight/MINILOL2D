using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Unit
{
    public class AttackBase : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _hitLayerMask;
        private Damage _damage;

        [SerializeField]
        private BoxCollider2D _attackCollider2D;

        protected ContactFilter2D _contactFilter2D;
        protected List<Collider2D> result = new List<Collider2D>();

        private bool _isInit = false;

        public virtual void Init(Damage damage)
        {
            _damage = damage;
            _contactFilter2D.useTriggers = true;
            _contactFilter2D.useLayerMask = true;
            _contactFilter2D.layerMask = _hitLayerMask;

            _isInit = true;
        }

        public virtual void Progress()
        {
            if (_isInit == false)
                return;

            AttackAreaDetection();
            AttackDamage();
        }


        private void AttackAreaDetection()
        {
            // 다음 프레임에 활성화가 되기 때문에 바로 끄면 체크 X
            if (_attackCollider2D.IsTouchingLayers(_hitLayerMask))
            {
                _attackCollider2D.OverlapCollider(_contactFilter2D, result);
                foreach (var item in result)
                {
                    if (item.gameObject.CompareTag("Player"))
                        continue;

                    if (item.gameObject.CompareTag("Enemy"))
                    {
                        break;
                    }
                }
            }
        }

        private void AttackDamage()
        {
            foreach (var item in result)
            {
                if (item.gameObject.CompareTag("Player"))
                    continue;

                if (item.gameObject.CompareTag("Enemy"))
                {
                    item.GetComponent<IDamageApplicable>().ApplyDamage(_damage);
                }
            }
        }
    }
}