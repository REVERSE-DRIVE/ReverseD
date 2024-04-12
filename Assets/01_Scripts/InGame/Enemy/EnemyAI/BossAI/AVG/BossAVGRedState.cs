namespace EnemyManage.EnemyBossBase
{
    public class BossAVGRedState : EnemyState<BossAVGStateEnum>
    {
        public BossAVGRedState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
    }
}