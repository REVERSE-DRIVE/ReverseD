namespace EnemyManage.EnemyBossBase
{
    public class BossAVGYellowState : EnemyState<BossAVGStateEnum>
    {
        public BossAVGYellowState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
    }
}