using System.Collections;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGStunState : BossAVGState
    {
        private float _stateDuration;
        private float _currentTime = 0;
        public BossAVGStunState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase.CanStateChangeable = false;
            _stateDuration = _bossAVGBase._stunDuration;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
            if (_currentTime >= _stateDuration)
            {
                _currentTime = 0;
                _stateMachine.ChangeState(BossAVGStateEnum.Idle, true);

            }
        }


        public override void Exit()
        {
            base.Exit();
            
            _bossAVGBase.CanStateChangeable = true;
        }
    }
}