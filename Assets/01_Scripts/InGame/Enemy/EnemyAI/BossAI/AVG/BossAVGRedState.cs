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
        private bool isPlayedSound;
        private CameraManager _camManagerCashing;
        
        public BossAVGRedState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _camManagerCashing = GameManager.Instance._CameraManager;
            _bossAVGBase._structureObject.Active(_bossAVGBase.transform);
            _bossAVGBase._chargingParticle.Play();
            SetDefault();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
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
            isPlayedSound = false;
        }

        private void Charge()
        {
            if (isChargeOver) return;
            
            _chargingLevel += Time.deltaTime * TimeManager.TimeScale * _chargingSpeed;
            _camManagerCashing.SetShake(_chargingLevel * 0.5f, 5);
            if (!isPlayedSound && _chargingLevel > _chargeEnergyAmount - 3.5f)
            {
                _bossAVGBase._soundObject.PlayAudio(4);
                isPlayedSound = true;

            }
            if (_bossAVGBase._chargingParticle.isPlaying && _chargingLevel > _chargeEnergyAmount - 3)
            {
                _bossAVGBase._chargingParticle.Stop();
            }
            if (_chargingLevel >= _chargeEnergyAmount)
            {
                isChargeOver = true;
                _chargingLevel = 0;
                BurstAttack();
            }
        }

        private void BurstAttack()
        {
            _stateMachine.ChangeState(BossAVGStateEnum.Idle);
            _camManagerCashing.ShakeOff();
            _camManagerCashing.Shake(40f, 1);
            
            _bossAVGBase.StartCoroutine(BurstOverCoroutine());
            
        }

        private IEnumerator BurstOverCoroutine()
        {
            _bossAVGBase._structureObject.OffObject();

            yield return new WaitForSeconds(0.2f);
            _bossAVGBase._burstParticle.Play();

            _bossAVGBase._structureObject.DefenseAVGBurst(_bossAVGBase._burstDamage);

        }
    }
}