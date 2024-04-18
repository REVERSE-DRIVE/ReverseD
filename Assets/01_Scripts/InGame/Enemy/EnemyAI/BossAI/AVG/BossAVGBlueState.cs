using System.Collections;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGBlueState : BossAVGState
    {
        
        private float _attackTime = 10;
        private float _attackCooltime = 0.2f;
        private int _projectileAmount;
        private Projectile _projectile;
        private int _currentPhaseLevel = 0;

        public BossAVGBlueState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }


        public override void Enter()
        {
            base.Enter();
            _attackTime = _bossAVGBase._attacktime;
            _attackCooltime = _bossAVGBase._attackCooltime;
            _projectileAmount = _bossAVGBase._fireProjectileAmount;
            _projectile = _bossAVGBase._projectile;
            _bossAVGBase.StartCoroutine(BlueStateRoutine());
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
        }

        public override void Exit()
        {
            base.Exit();
        }


        private IEnumerator BlueStateRoutine()
        {
            yield return new WaitForSeconds(1f);
            float currentTime = 0;
            float currentCoolingTime = 0;
            int rotationDirection = 1;

            while (currentTime <= _attackTime)
            {
                if(TimeManager.TimeScale == 0)
                    continue;
                
                currentTime += Time.deltaTime;
                _bossAVGBase.transform.rotation = 
                    Quaternion.Euler(0, 0, 
                        _bossAVGBase.transform.rotation.eulerAngles.z +(rotationDirection)*_bossAVGBase._rotationSpeed);
                currentCoolingTime += Time.deltaTime * TimeManager.TimeScale;
                if (currentCoolingTime > _attackCooltime)
                {
                    currentCoolingTime = 0;
                    Attack();
                }
 
                if (currentTime >= _attackTime * 0.5f)
                {
                    rotationDirection = -1;
                }

                yield return null;
            }

            _bossAVGBase.StartCoroutine(EndCoroutine());
        }

        private IEnumerator EndCoroutine()
        {
            float currentTime = 0;
            float beforeRotation = _bossAVGBase.transform.rotation.eulerAngles.z;
            while (currentTime <= 0.2f)
            {
                float time = currentTime / 0.2f;
                _bossAVGBase.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0,0,beforeRotation),Quaternion.identity,  time);
                currentTime += Time.deltaTime;
                yield return null;
            }
            _bossAVGBase.transform.rotation = Quaternion.identity;
            _stateMachine.ChangeState(BossAVGStateEnum.Idle);
        }

        private void Attack()
        {
            float angleStep = 360f / _projectileAmount; // 각도 단계 계산

            for (int i = 0; i < _projectileAmount; i++)
            {
                // 각도 계산
                float angle = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
                direction = RotateVector2(direction, _bossAVGBase.transform.rotation.eulerAngles.z);
                Projectile projectile = PoolManager.Get(_projectile);
                projectile.transform.position = _bossAVGBase.transform.position;
                projectile.Fire(_bossAVGBase.transform.position, direction);
                
            }
        }
        Vector2 RotateVector2(Vector2 vec, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
            float x = vec.x;
            float y = vec.y;
            vec.x = x * cos - y * sin;
            vec.y = x * sin + y * cos;
            return vec;
        }
    }
}