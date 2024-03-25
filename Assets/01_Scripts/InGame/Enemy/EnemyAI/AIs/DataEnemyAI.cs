using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage.AIs
{
    
    public class DataEnemyAI : NormalAI
    {
        [Header("Attack Setting Values")] [SerializeField]
        private float _attackCycleSpeed = 3;

        [SerializeField] private GameObject _projectile;

        protected override void Attack()
        {
            Vector2 attackDirection = (_playerTrm.position - transform.position);

            GameObject bullet = PoolManager.Get(_projectile);
            //bullet. 

        }
    }
}
