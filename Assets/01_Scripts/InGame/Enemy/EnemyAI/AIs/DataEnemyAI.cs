using System.Collections;
using System.Collections.Generic;
using EnemyManage.EnemyBase;
using UnityEngine;

namespace EnemyManage.AIs
{
    
    public class DataEnemyAI : NormalAI
    {
        [Header("Attack Setting Values")] [SerializeField]
        private float _attackCycleSpeed = 3;

        [SerializeField] private GameObject _projectile;
        private NormalEnemy _normalEnemy;
        
        private readonly int IdleHash = Animator.StringToHash("Idle");
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private readonly int DieHash = Animator.StringToHash("Die");
        
        protected override void Start()
        {
            base.Start();
            _normalEnemy = GetComponent<NormalEnemy>();
            _normalEnemy.AnimatorCompo.SetBool(IdleHash, true);
            _normalEnemy.OnDieEvent += HandleDieEvent;
        }

        protected override void Update()
        {
            base.Update();
            EnemySpriteFlip();
        }

        private void EnemySpriteFlip()
        {
            if (_playerTrm.position.x < transform.position.x)
            {
                _normalEnemy._spriteRenderer.flipX = true;
            }
            else
            {
                _normalEnemy._spriteRenderer.flipX = false;
            }
        }

        private void HandleDieEvent()
        {
            _normalEnemy.AnimatorCompo.SetBool(IdleHash, false);
            _normalEnemy.AnimatorCompo.SetBool(AttackHash, false);
            _normalEnemy.AnimatorCompo.SetBool(DieHash, true);
            
            SetDefault();
        }


        protected override void Attack()
        {
            _normalEnemy.AnimatorCompo.SetBool(IdleHash, false);
            _normalEnemy.AnimatorCompo.SetBool(AttackHash, true);
            Vector2 attackDirection = (_playerTrm.position - transform.position);
            
            GameObject bullet = PoolManager.Get(_projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Projectile>().Fire(attackDirection);

        }
    }
}
