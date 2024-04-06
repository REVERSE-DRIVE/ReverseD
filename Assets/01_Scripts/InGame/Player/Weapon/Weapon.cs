using System;
using UnityEngine;

namespace AttackManage
{
    public abstract class Weapon : MonoBehaviour
    {

        internal int damage = 3;
        internal float _attackCooltime = 1f;
        internal float _attackTime = 1;

        [Header("Dev Setting")]
        [Range(-180, 180)]
        [SerializeField]
        private float _rotationOffset = 0;
        [SerializeField]
        protected Animator _weaponAnimator;
        [Space(10)]
        [Header("State Information")]
        public bool isRotation = true;
        [SerializeField]
        protected Vector2 attackDirection;

        protected LayerMask _whatIsEnemy;
        
        protected virtual void Awake()
        {
            _whatIsEnemy= LayerMask.GetMask("Enemy");
            if (_weaponAnimator == null)
            {
                _weaponAnimator.GetComponent<Animator>();
            }
        }

        protected virtual void OnEnable()
        {
            
        }
       
        protected virtual void OnDisable()
        {
        }

        /**
         * <summary>
         * 공격이 실행 되었을때 코드를 완성해야함
         * 쿨타임과 같은 부가적인 체크는 PlayerAttackController에서 함
         * </summary>
         */
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

        public virtual void WeaponRotateHandler(Vector2 direction)
        {
            if (!isRotation) return;
            if (direction.sqrMagnitude == 0)
                return;
            // 오프셋 부분 수정해야될 수도 있움
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _rotationOffset);
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