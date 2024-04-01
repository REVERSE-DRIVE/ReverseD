using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage.AIs
{
    
    public class DataEliteEnemyAI : EliteAI
    {
        [Header("Attack Setting Values")] [SerializeField]
        private float _attackCycleSpeed = 3;

        [SerializeField] private GameObject _projectile;


        protected override void Attack()
        {
            
            
        }

        private IEnumerator AttackRoutine()
        {
            
            yield return new WaitForSeconds(_attackCycleSpeed);
        }

        private void Shoot()
        {
                
        }
        
        
    }
}
