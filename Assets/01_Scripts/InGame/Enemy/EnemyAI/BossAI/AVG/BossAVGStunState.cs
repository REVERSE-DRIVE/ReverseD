namespace EnemyManage.EnemyBossBase
{
    public class BossAVGStunState : BossAVGState
    {
        public BossAVGStunState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
    }
}