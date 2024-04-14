using System.Collections;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGRedState : BossAVGState
    {
        private int _chargeEnergyAmount = 10;
        private float _chargingLevel = 0;
        private float _chargingSpeed = 1;
        private bool isChargeOver;
        private CameraManager _camManagerCashing;
        public BossAVGRedState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _camManagerCashing = GameManager.Instance._CameraManager;
            SetDefault();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (TimeManager.TimeScale == 0) return;
            
            Charge();

        }


        public override void Exit()
        {
            base.Exit();
        }

        private void SetDefault()
        {
            _chargingLevel = 0;
            _chargeEnergyAmount = _bossAVGBase._chargeEnergy;
            _chargingSpeed = _bossAVGBase._chargingSpeed;
            isChargeOver = false;
        }

        private void Charge()
        {
            if (isChargeOver) return;
            
            _chargingLevel += Time.deltaTime * TimeManager.TimeScale * _chargingSpeed;
            _camManagerCashing.SetShake(_chargingLevel * 0.5f, 5);

            if (_chargingLevel >= _chargeEnergyAmount)
            {
                isChargeOver = true;
                _chargingLevel = 0;
                BurstAttack();
            }
        }

        private void BurstAttack()
        {
            _camManagerCashing.ShakeOff();
            _bossAVGBase._structureObject.DefenseAVGBurst(_bossAVGBase._burstDamage);
            _bossAVGBase.StartCoroutine(BurstOverCoroutine());
        }

        private IEnumerator BurstOverCoroutine()
        {
            yield return new WaitForSeconds(2);
            _stateMachine.ChangeState(BossAVGStateEnum.Idle);
        }
    }
}