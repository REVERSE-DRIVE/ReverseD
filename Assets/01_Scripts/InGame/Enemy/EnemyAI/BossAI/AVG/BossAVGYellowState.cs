namespace EnemyManage.EnemyBossBase
{
    public class BossAVGYellowState : BossAVGState
    {
        public BossAVGYellowState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase._isResist = true;
        }
        
        private void Break


        public override void Exit()
        {
            base.Exit();
            _bossAVGBase._isResist = false;
        }
    }
}