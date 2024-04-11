namespace EnemyManage.BossAI
{
    public class BossAVGBlueState : EnemyState<BossAVGStateEnum>
    {
        public BossAVGBlueState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}