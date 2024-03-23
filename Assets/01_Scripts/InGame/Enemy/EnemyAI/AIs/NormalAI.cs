using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyManage.AIs
{
    public class NormalAI : EnemyAI
    {
        private Transform _playerTrm;
        private bool isRoaming = false;
        private bool isAttacking = false;
        private bool isStun = false;

        [Header("Setting Values")] 
        [SerializeField]
        private float followDistance = 5;
        [SerializeField] private float _playerDetectDistance = 10;
        [SerializeField] private float RoamingDuration = 1.5f;
        [SerializeField] private float RoamingCoolTime = 2;

        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Start()
        {
            _playerTrm = GameManager.Instance._PlayerTransform;
        }

        protected override void Move()
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
                    Attack();
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerArrow"))
            {
                _currentState = EnemyStateEnum.Stun;
            }
        }
        
        #region Waiting
        private void Waiting()
        {
            StartCoroutine(WaitingCoroutine());
        }

        private IEnumerator WaitingCoroutine()
        {
            if (_currentState != EnemyStateEnum.Waiting) yield break;
            yield return new WaitForSeconds(1f);
            _currentState = EnemyStateEnum.Roaming;
        }
        #endregion
        
        #region Stun
        private void Stun()
        {
            StartCoroutine(StunCoroutine());
        }
        
        private IEnumerator StunCoroutine()
        {
            if (isStun) yield break;
            isStun = true;
            _rigid.velocity = Vector2.zero;
            yield return new WaitForSeconds(4f);
            _currentState = EnemyStateEnum.Waiting;
        }
        #endregion

        #region Roaming
        private void Roaming()
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
                _rigid.velocity = direction.normalized * (_enemy.status.moveSpeed * TimeManager.TimeScale);
                yield return new WaitForSeconds(0.5f);
                _rigid.velocity = Vector2.zero;
                yield return new WaitForSeconds(2f);
            }
        }

        protected override void DetectPlayer()
        {
            RaycastHit2D ray = Physics2D.CircleCast(transform.position, _playerDetectDistance, Vector2.zero, 0);
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    _currentState = EnemyStateEnum.Attack;
                    Debug.Log("Attack");
                }
            }
        }
        #endregion
        
        #region Attacking
        protected override void Attack()
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
                _rigid.velocity = direction * (_enemy.status.moveSpeed * TimeManager.TimeScale);
            }
            if (Vector3.Distance(_playerTrm.position, transform.position) > _playerDetectDistance)
            {
                _currentState = EnemyStateEnum.Roaming;
            }
        }
        
        private IEnumerator AttackCoroutine()
        {
            if (isAttacking) yield break;
            isAttacking = true;
            while (true)
            {
                if (TimeManager.TimeScale == 0) yield break;
                PlayerManager.Instance.PlayerHealth--;
                yield return new WaitForSeconds(1f);
            }
        }
        #endregion
        
    }
}
