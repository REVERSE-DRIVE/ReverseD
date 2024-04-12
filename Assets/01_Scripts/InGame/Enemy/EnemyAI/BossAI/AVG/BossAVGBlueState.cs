using System.Collections;
using EnemyManage;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGBlueState : BossAVGState
    {
        public BossAVGBlueState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        private float _attackTime = 10;
        private float _attackCooltime = 0.2f;
        

        public override void Enter()
        {
            base.Enter();
            _attackTime = _bossAVGBase._attacktime;
            _attackCooltime = _bossAVGBase._attackCooltime;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
        }


        private IEnumerator BlueStateRoutine()
        {
            float currentTime = 0;
            float currentCoolingTime = 0;
            while (currentTime >= _attackTime)
            {

                currentTime += Time.deltaTime;
                currentCoolingTime += Time.deltaTime;
                if (currentCoolingTime > _attackCooltime)
                {
                    currentCoolingTime = 0;
                    
                }

                yield return null;
            }
        }

        private void Attack()
        {
            
        }
    }
}