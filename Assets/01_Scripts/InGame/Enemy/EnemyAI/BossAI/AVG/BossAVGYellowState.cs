using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGYellowState : BossAVGState
    {
        private float _stateDuration;
        private float _currentTime = 0;
        
        public BossAVGYellowState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase._isResist = true;
            _stateDuration = _bossAVGBase._yellowStateDuration;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime * TimeManager.TimeScale;
            if (_currentTime >= _stateDuration)
            {
                BreakState();
            }
        }

        internal void BreakState()
        {
            _bossAVGBase._isResist = false;
            _bossAVGBase.ForceStun();
            
        }

        public override void CustomTrigger()
        {
            BreakState();

        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}