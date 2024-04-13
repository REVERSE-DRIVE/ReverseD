namespace EnemyManage.EnemyBossBase
{
    public class BossAVGState : EnemyState<BossAVGStateEnum>
    {
        protected BossAVG _bossAVGBase;
        public BossAVGState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase = (BossAVG)_enemyBase;
        }
    }
}