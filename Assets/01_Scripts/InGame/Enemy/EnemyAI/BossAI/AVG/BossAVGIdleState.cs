using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGIdleState : BossAVGState
    {
        public BossAVGIdleState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
        
        
        public override void UpdateState()
        {
            base.UpdateState();
            
            
        }
    }
}