﻿using EnemyManage;
using UnityEngine;
using UnityEngine.Serialization;

namespace AttackManage
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Weapon CustomSetting")]
        [SerializeField] internal int damage = 3;
        [SerializeField] internal float _attackCooltime = 1f;
        [SerializeField] internal float _attackTime = 1;
        [SerializeField] internal bool _useAutoAiming;
        [SerializeField] internal float _autoAimingDistance = 1;
        
        [SerializeField] internal bool _isAutoTargeted;
        [SerializeField] internal bool _isKnockBack;
        [SerializeField] internal float _knockBackPower = 1;
        
        [Header("Dev Setting")]
        [Range(-180, 180)]
        [SerializeField]
        protected float _rotationOffset = 0;
        [SerializeField]
        protected Animator _weaponAnimator;
        [Space(10)]
        [Header("State Information")]
        public bool isRotation = true;
        [SerializeField]
        protected Vector2 attackDirection;

        [SerializeField]
        protected LayerMask _whatIsTarget;
        
        
        
        protected virtual void Awake()
        {
            if (_weaponAnimator == null)
            {
                _weaponAnimator.GetComponent<Animator>();
            }
        }


        /**
         * <summary>
         * 공격이 실행 되었을때 코드를 완성해야함
         * 쿨타임과 같은 부가적인 체크는 PlayerAttackController에서 알아서 함
         * </summary>
         */
        public abstract void AttackStart();

        public abstract void AttackEnd();
        
        public virtual void AttackAnimationOnTrigger()
        {
            _weaponAnimator.SetBool("IsAttack", true);
        }
        
        public virtual void AttackAnimationOffTrigger()
        {
            _weaponAnimator.SetBool("IsAttack", false);
        }

        public virtual void WeaponRotateHandler(Vector2 direction)
        {
            
            if (!isRotation) return;
            if (direction.sqrMagnitude == 0)
                return;
            attackDirection = direction;
            // 오프셋 부분 수정해야될 수도 있움
            Quaternion rotate = Quaternion.Euler(0, 0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _rotationOffset);
            transform.rotation = rotate;
            if (Mathf.Abs(rotate.z) > 0.7f)  // z rotation값 재계산 해야함
            {
                transform.parent.localScale = new Vector2(-1, 1);
                transform.localScale = -Vector2.one;
            }
            else
            {
                transform.parent.localScale = Vector2.one;
                transform.localScale = Vector2.one;
            }
        }

        protected virtual void TakeDamageToTargets(Collider2D[] hits)
        {
            if (hits == null) return;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (_isKnockBack)
                    {
                        enemy.TakeDamageWithKnockBack(
                            damage, GameManager.Instance._PlayerTransform.position,
                            _knockBackPower);
                        continue;
                    }
                    enemy.TakeDamage(damage);
                }
                else if(hits[i].TryGetComponent<FieldObject>(out FieldObject fieldObject))
                {
                    fieldObject.TakeDamage(damage);
                }
                    
            }
        }

        
    }
}