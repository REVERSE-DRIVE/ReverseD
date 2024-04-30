using EnemyManage;
using UnityEngine;

namespace AttackManage
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Weapon CustomSetting")]
        [SerializeField] internal int damage = 3;
        [SerializeField] internal float _attackCooltime = 1f;
        [SerializeField] internal float _attackTime = 1;
        [SerializeField] internal bool _useAutoAiming;
        [SerializeField] internal float _autoAimingDistance = 1;
        
        [SerializeField] internal bool _isAutoTargeted;
        [SerializeField] internal bool _isKnockBack;
        [SerializeField] internal float _knockBackPower = 1;
        
        [Header("Dev Setting")]
        [Range(-180, 180)]
        [SerializeField]
        protected float _rotationOffset = 0;
        [SerializeField]
        protected Animator _weaponAnimator;
        [Space(10)]
        [Header("State Information")]
        public bool isRotation = true;
        [SerializeField]
        protected Vector2 attackDirection;

        [SerializeField] protected LayerMask _wallLayer;

        [SerializeField]
        protected LayerMask _whatIsTarget;
        
        
        
        protected virtual void Awake()
        {
            if (_weaponAnimator == null)
            {
                _weaponAnimator = GetComponent<Animator>();
            }

            _wallLayer = LayerMask.GetMask("Wall");
        }


        /**
         * <summary>
         * 공격이 실행 되었을때 코드를 완성해야함
         * 쿨타임과 같은 부가적인 체크는 PlayerAttackController에서 알아서 함
         * </summary>
         */
        public abstract void AttackStart();

        public abstract void AttackEnd();
        
        public virtual void AttackAnimationOnTrigger()
        {
            _weaponAnimator.SetBool("IsAttack", true);
        }
        
        public virtual void AttackAnimationOffTrigger()
        {
            _weaponAnimator.SetBool("IsAttack", false);
        }

        public virtual void WeaponRotateHandler(Vector2 direction)
        {
            
            if (!isRotation) return;
            if (direction.sqrMagnitude == 0)
                return;
            attackDirection = direction;
            // 오프셋 부분 수정해야될 수도 있움
            Quaternion rotate = Quaternion.Euler(0, 0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _rotationOffset);
            transform.rotation = rotate;
            if (Mathf.Abs(rotate.z) > 0.7f)  // z rotation값 재계산 해야함
            {
                transform.parent.localScale = new Vector2(-1, 1);
                transform.localScale = -Vector2.one;
            }
            else
            {
                transform.parent.localScale = Vector2.one;
                transform.localScale = Vector2.one;
            }
        }

        protected virtual void TakeDamageToTargets(Collider2D[] hits)
        {
            if (hits == null) return;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (_isKnockBack)
                    {
                        enemy.TakeDamageWithKnockBack(
                            damage, GameManager.Instance._PlayerTransform.position,
                            _knockBackPower);
                        continue;
                    }
                    enemy.TakeDamage(damage);
                }
                else if(hits[i].TryGetComponent<FieldObject>(out FieldObject fieldObject))
                {
                    fieldObject.TakeDamage(damage);
                }
                    
            }
        }

        protected virtual void AutoAim()
        {
            Vector2 currentPosition = transform.position;
            if (_useAutoAiming)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(currentPosition, _autoAimingDistance, _whatIsTarget); // 현재 위치 주변의 적을 검색

                Transform closestEnemy = null;
                float closestDistance = 99;

                // 모든 적에 대해 가장 가까운 적 찾기
                foreach (Collider2D enemyCollider in enemies)
                {
                    Vector2 direction = ((Vector2)enemyCollider.transform.position - currentPosition);
                    RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction.normalized, _autoAimingDistance, _wallLayer); // 레이 발사하여 장애물 감지

                    if (hit.collider == null) // 장애물이 없는 경우
                    {
                        if (direction.magnitude < closestDistance)
                        {
                            closestDistance = direction.magnitude;
                            closestEnemy = enemyCollider.transform;
                        }
                    }
                }

                if (closestEnemy != null)
                {
                    // 주어진 위치와 가장 가까운 적의 위치 사이의 방향 벡터 반환
                    
                    attackDirection = ((Vector2)closestEnemy.position - currentPosition).normalized;
                    WeaponRotateHandler(attackDirection);
                    _isAutoTargeted = true;
                }
                else
                {
                    // 가장 가까운 적이 없으면 기본 방향 벡터 반환 (예: 위쪽)
                    _isAutoTargeted = false;

                }

            }
        }
        private void OnDrawGizmos()
        {
            if (_useAutoAiming)
            {
                if (_isAutoTargeted)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;

                }
                Gizmos.DrawWireSphere(transform.position, _autoAimingDistance);
            }
        }
    }
}