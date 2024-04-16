using System.Collections;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGGreenState : BossAVGState
    {
        private float _currentTime = 0;
        private float _currentStateTime = 0;
        private float _stateDuration;
        public BossAVGGreenState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _currentTime = 0;
            _currentStateTime = 0;
            _stateDuration = _bossAVGBase._greenStateDuration;

            OnAllHealingCore();
            _bossAVGBase.OnHealDefense();
        }

        private void OnAllHealingCore()
        {
            for (int i = 0; i < _bossAVGBase._healingObjects.Length; i++)
            {
                _bossAVGBase._healingObjects[i].OnCore();
            }
        }

        private void OffAllHealingCore()
        {
            for (int i = 0; i < _bossAVGBase._healingObjects.Length; i++)
            {
                if (_bossAVGBase._healingObjects[i].isActive)
                {
                    _bossAVGBase._healingObjects[i].Destroy();

                }
            }
        }

        public override void UpdateState()
        {
            if (TimeManager.TimeScale == 0) return;
            float time = Time.deltaTime * TimeManager.TimeScale;
            _currentTime += time;
            _currentStateTime += time;
            if (_currentTime >= 1)
            {
                _currentTime = 0;
                HealWithCore();
            }

            if (_currentStateTime >= _stateDuration)
            {
                _stateMachine.ChangeState(BossAVGStateEnum.Idle);
            }
        }

        private void HealWithCore()
        {
            int totalHeal = 0;
            for (int i = 0; i < _bossAVGBase._healingObjects.Length; i++)
            {
                if (_bossAVGBase._healingObjects[i].isActive)
                {
                    totalHeal += _bossAVGBase._healCoreHealAmountPerSecond;
                } 
            }
            _bossAVGBase.RestoreHealth(totalHeal);
        }


        
        public override void Exit()
        {
            base.Exit();
            OffAllHealingCore();
            _bossAVGBase.OffHealDefense();
        }
    }
}