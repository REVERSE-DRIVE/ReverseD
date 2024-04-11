namespace EnemyManage.BossAI
{
    public class BossAVGGreenState : EnemyState<BossAVGStateEnum>
    {
        public BossAVGGreenState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
        
        
    }
}