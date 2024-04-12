namespace EnemyManage.EnemyBossBase
{
    public class BossAVGStunState : EnemyState<BossAVGStateEnum>
    {
        public BossAVGStunState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
    }
}