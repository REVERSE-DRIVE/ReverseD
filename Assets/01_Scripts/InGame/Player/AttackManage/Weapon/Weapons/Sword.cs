using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public abstract class Sword : Weapon
    {
        [Header("Sword Setting")]
        [SerializeField] public float _attackRadius = 2.5f;
        [SerializeField] protected bool _isSplash = true;
        [SerializeField] protected float _damageTiming;
        [SerializeField] protected Transform _attackParticleHandleTrm;
        [SerializeField] protected ParticleSystem _attackParticle;
        protected Transform _particleTrm;
        protected Vector2 _DetectCenterPos;
        // 실질적인 Attack 기능을 하위 무기에서 구현

        protected override void Awake()
        {
            base.Awake();
            _particleTrm = _attackParticle.transform;
        }

        public override void WeaponRotateHandler(Vector2 direction)
        {
            if (!isRotation) return;
            if (direction.sqrMagnitude == 0)
                return;
            attackDirection = direction;
            // 오프셋 부분 수정해야될 수도 있움
            Quaternion rotate = Quaternion.Euler(0, 0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _rotationOffset);
            transform.rotation = rotate;
            //_attackParticle.startRotation = rotate.z;
            //_particleTrm.rotation = rotate;
            if (Mathf.Abs(rotate.z) > 0.7f)  // z rotation값 재계산 해야함
            {
                transform.parent.localScale = new Vector2(-1, 1);
                transform.localScale = -Vector2.one;
                _particleTrm.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.parent.localScale = Vector2.one;
                transform.localScale = Vector2.one;
                _particleTrm.localScale = Vector2.one;
            }
        }
        
        /**
         * <summary>
         * 실질적으로 데미지를 적과 타겟에게 입히는 타이밍을 구현
         * </summary>
         */
        protected abstract IEnumerator AttackCoroutine();

        /**
         * <summary>
         * 공격 이펙트를 재생한다
         * </summary>
         */
        protected virtual void ShowAttackEffect()
        {
            //_attackParticle.transform.position = _DetectCenterPos;
            
            _attackParticle.Play();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_DetectCenterPos, _attackRadius);
        }
        
        protected abstract Collider2D[] DetectTargets();

    }
}