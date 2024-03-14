using System;
using System.Collections;
using UnityEngine;
using entityManage;
using Random = UnityEngine.Random;

namespace EnemyManage.AIs
{
    public class NormalAI : EnemyAI
    {
        [SerializeField] private float _radius = 5;
        private Player _player;
        private bool isRoaming = false;
        private bool isAttacking = false;

        protected override void Awake()
        {
            base.Awake();
            _player = FindObjectOfType<Player>();
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
            }
        }

        private void Attack()
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            
            if (Vector3.Distance(_player.transform.position, transform.position) < 1f)
            {
                _rigid.velocity = Vector2.zero;
                StartCoroutine(nameof(AttackCoroutine));
            }
            else
            {
                StopCoroutine(nameof(AttackCoroutine));
                isAttacking = false;
                _rigid.velocity = direction * 5f;
            }
            if (Vector3.Distance(_player.transform.position, transform.position) > _radius)
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
                PlayerManager.Instance.PlayerHealth--;
                yield return new WaitForSeconds(1f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void Roaming()
        {
            StartCoroutine(RoamingCoroutine());
        }
        
        private IEnumerator RoamingCoroutine()
        {
            if (isRoaming) yield break;
            isRoaming = true;
            while (true)
            {
                DetectPlayer();
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                _rigid.velocity = direction.normalized * 5f;
                yield return new WaitForSeconds(0.5f);
                _rigid.velocity = Vector2.zero;
                yield return new WaitForSeconds(2f);
            }
        }

        protected override void DetectPlayer()
        {
            RaycastHit2D ray = Physics2D.CircleCast(transform.position, _radius, Vector2.zero, 0);
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    _currentState = EnemyStateEnum.Attack;
                    Debug.Log("Attack");
                }
            }
        }
        
    }
}