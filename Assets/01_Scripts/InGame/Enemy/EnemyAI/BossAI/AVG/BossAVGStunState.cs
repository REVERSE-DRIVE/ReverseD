using System.Collections;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGStunState : BossAVGState
    {
        public BossAVGStunState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase.CanStateChangeable = false;
            _bossAVGBase.StartCoroutine(StunCoroutine());
        }

        private IEnumerator StunCoroutine()
        {
            yield return new WaitForSeconds(_bossAVGBase._stunDuration);
            _stateMachine.ChangeState(BossAVGStateEnum.Idle, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            _bossAVGBase.CanStateChangeable = true;
        }
    }
}