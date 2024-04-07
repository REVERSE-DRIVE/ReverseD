using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyManage
{
    
    public abstract class EnemyAI : MonoBehaviour
    {
        protected Transform _playerTrm;

        protected Enemy _enemy;
        [Header("Current State Values")]
        [Space(10)]
        [SerializeField] protected bool _isStatic;
        [SerializeField] protected EnemyStateEnum _currentState;
        [SerializeField] protected bool isRoaming = false;
        [SerializeField] protected bool isAttacking = false;
        [SerializeField] protected bool isStun = false;

        
        [Header("Setting Values")] 
        [Space(10)]
        [SerializeField]
        [Tooltip("공격 대상과 우선순위를 나타냄")]
        protected EnemyTargetingTaget[] _targetingObject;

        [SerializeField] protected float _attackCoolTime = 1.5f;
        [SerializeField] protected float followDistance = 5;
        [SerializeField] protected float _playerDetectDistance = 10;
        [SerializeField] protected float _roamingDuration = 1.5f;
        [SerializeField] protected float _roamingCoolTime = 2;
        [SerializeField] protected float _stunDuration = 2;

        protected Rigidbody2D _rigid;
        private LayerMask _playerLayer;


        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _enemy = GetComponent<Enemy>();
            _playerLayer = LayerMask.GetMask("Player");
        }

        protected virtual void Start()
        {
            _playerTrm = GameManager.Instance._PlayerTransform;

        }

        protected void Update()
        {
            EnemyRoutine();
        }


        protected virtual void EnemyRoutine()
        {
            
            if (_isStatic)
            {
                return;
            }
            switch (_currentState)
            {
                case EnemyStateEnum.Roaming:
                    isAttacking = false;
                    Roaming();
                    break;
                case EnemyStateEnum.Attack:
                    isRoaming = false; 
                    ChasePlayer();
                    break;
                case EnemyStateEnum.Stun:
                    Stun();
                    break;
                case EnemyStateEnum.Waiting:
                    isStun = false;
                    Waiting();
                    break;
            }
        }
        #region Waiting
        protected void Waiting()
        {
            StartCoroutine(WaitingCoroutine());
        }

        protected IEnumerator WaitingCoroutine()
        {
            if (_currentState != EnemyStateEnum.Waiting) yield break;
            yield return new WaitForSeconds(1f);
            _currentState = EnemyStateEnum.Roaming;
        }
        #endregion
        
        #region Stun
        protected void Stun()
        {
            StartCoroutine(StunCoroutine());
        }
        
        protected IEnumerator StunCoroutine()
        {
            if (isStun) yield break;
            isStun = true;
            _rigid.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stunDuration);
            _currentState = EnemyStateEnum.Waiting;
        }
        #endregion
        #region Attacking

        protected virtual void ChasePlayer()
        {
            Vector3 direction = (_playerTrm.position - transform.position).normalized;
            
            if (Vector3.Distance(_playerTrm.position, transform.position) < followDistance)
            {
                _rigid.velocity = Vector2.zero;
                StartCoroutine(nameof(AttackCoroutine));
            }
            else
            {
                StopCoroutine(nameof(AttackCoroutine));
                isAttacking = false;
                _rigid.velocity = direction * (_enemy.Status.moveSpeed * TimeManager.TimeScale);
            }
        }
        protected abstract void Attack();


        protected virtual IEnumerator AttackCoroutine()
        {
            if (isAttacking) yield break;
            isAttacking = true;
            while (true)
            {
                yield return new WaitForSeconds(_attackCoolTime);

                if (TimeManager.TimeScale == 0) yield break;
                //PlayerManager.Instance.PlayerHealth--;
                Attack();
            }
        }
        #endregion
        
        
        
        #region Roaming

        protected virtual void Roaming()
        {
            if (!isRoaming)
            {
                StartCoroutine(RoamingCoroutine());
                isRoaming = true;
            }
        }
        private IEnumerator RoamingCoroutine()
        {
            if (isStun)
            {
                yield break;
            }
            
            while (true)
            {
                DetectPlayer();
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                Move(direction);
                yield return new WaitForSeconds(_roamingDuration);
                _rigid.velocity = Vector2.zero;
                yield return new WaitForSeconds(_roamingCoolTime);
            }
        }
        
        #endregion
        
        protected virtual void DetectPlayer()
        {
            RaycastHit2D ray = Physics2D.CircleCast(
                transform.position, _playerDetectDistance, 
                Vector2.zero, 0, _playerLayer);
            if (ray.collider != null)
            {
                _currentState = EnemyStateEnum.Attack;
            }
        }
        
        protected virtual void Move(Vector2 dir)
        {
            _rigid.velocity = dir.normalized * (_enemy.Status.moveSpeed * TimeManager.TimeScale);
        }

        public virtual void SetDefault()
        {
            _currentState = EnemyStateEnum.Roaming;
            isRoaming = false;
            isAttacking = false;
            isStun = false;
            //_enemy.SetStatusDefault();
        }
    }
}
