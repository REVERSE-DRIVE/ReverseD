using System.Collections;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGGreenState : BossAVGState
    {
        public BossAVGGreenState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase.StartCoroutine(StateCoroutine());
        }


        private IEnumerator StateCoroutine()
        {
            _bossAVGBase.OnHealDefense();
            yield return new WaitForSeconds(_bossAVGBase._greenStateDuration);
            _stateMachine.ChangeState(BossAVGStateEnum.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            _bossAVGBase.OffHealDefense();
        }
    }
}