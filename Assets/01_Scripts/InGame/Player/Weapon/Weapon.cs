using System;
using UnityEngine;

namespace AttackManage
{
    public abstract class Weapon : MonoBehaviour
    {
        public Action<Vector2> OnMoveDirectionEvent;

        internal int damage = 3;
        internal float _attackCooltime = 1f;

        [SerializeField]
        protected Animator _weaponAnimator;
        public bool isRotation;

        protected void Awake()
        {
            if (_weaponAnimator == null)
            {
                _weaponAnimator.GetComponent<Animator>();
            }
        }

        protected virtual void Start()
        {
            OnMoveDirectionEvent += WeaponRotateHandler;
        }

        protected virtual void OnDisable()
        {
            OnMoveDirectionEvent -= WeaponRotateHandler;
        }

        public abstract void Attack();

        public abstract void AttackEnd();
        
        protected virtual void AttackAnimationOnTrigger()
        {
            _weaponAnimator.SetBool("IsAttack", true);
        }
        
        protected virtual void AttackAnimationOffTrigger()
        {
            _weaponAnimator.SetBool("IsAttack", false);
        }

        protected virtual void WeaponRotateHandler(Vector2 direction)
        {
            if (!isRotation) return;
            if (direction.sqrMagnitude == 0)
                return;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            if (Mathf.Abs(transform.rotation.z) > 0.7f)
            {
                transform.localScale = new Vector2(1, -1);
            }
            else
            {
                transform.localScale = Vector2.one;
            }
        }
    }
}