using EnemyManage.BossAI;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVG : Boss
    {
        public EnemyStateMachine<BossAVGState> StateMachine { get; private set; }

        public override void AnimationEndTrigger()
        {
            
        }
    }
}