namespace EnemyManage.EnemyBase
{
    public class NormalEnemy : Enemy
    {
        public EnemyStateMachine<NormalEnemyStateEnum> StateMachine { get; private set; }

        public override void AnimationEndTrigger()
        {
            throw new System.NotImplementedException();
        }
    }
}