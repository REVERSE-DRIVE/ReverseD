using System;
using UnityEngine;

namespace AttackManage
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int damage = 3;
        [SerializeField] protected float _attackCooltime = 1f;


        protected Animator _weaponAnimator;


        protected void Awake()
        {
            if (_weaponAnimator == null)
            {
                _weaponAnimator.GetComponent<Animator>();
            }
        }

        public abstract void Attack();

        protected void AttackAnimationTrigger()
        {
            
        }

        protected virtual void WeaponRotate()
        {
            
        }
    }
}