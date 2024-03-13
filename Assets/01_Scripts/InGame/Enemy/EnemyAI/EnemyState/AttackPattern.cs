using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using entityManage;
using UnityEngine;

namespace EnemyManage
{
    public class AttackPattern : EnemyAttackPattern
    {
        private Player _player;
        private EnemyAI _enemyAI;
        
        public override void Enter()
        {
        }
        
        public override void Update()
        {
        }
        
        public override void Exit()
        {
            state = EnemyStateEnum.Roaming;
        }
        
        private void Attack()
        {
            Status status = _player.Status;
            status.hp -= 1;
            _player.Status = status;
        }
        
    }
}


