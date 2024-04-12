namespace EnemyManage.EnemyBossBase
{
    public class BossAVGRedState : BossAVGState
    {
        public BossAVGRedState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
            
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}