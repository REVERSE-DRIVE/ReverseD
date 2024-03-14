using System.Collections;
using UnityEngine;
using EnemyManage;
using entityManage;

namespace EnemyManage.AIs
{
    public class NormalAI : EnemyAI
    {
        private bool isRoaming = false;

        protected override void Awake()
        {
            base.Awake();
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
                    Roaming();
                    break;
                case EnemyStateEnum.Attack:
                    isRoaming = false;
                    Attack();
                    break;
                case EnemyStateEnum.Waiting:
                    isRoaming = false;
                    Waiting();
                    break;
            }
        }

        private void Waiting()
        {
            RaycastHit2D ray =Physics2D.CircleCast(transform.position, 5, Vector2.zero, 0);
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    _currentState = EnemyStateEnum.Attack;
                    Debug.Log("Attack");
                }
            }
        }

        private void Attack()
        {
            RaycastHit2D ray =Physics2D.CircleCast(transform.position, 5, Vector2.zero, 0);
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    Status status = ray.collider.GetComponent<Player>().Status;
                    status.hp -= 1;
                    ray.collider.GetComponent<Player>().Status = status;
                }
            }
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
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                _rigid.velocity = direction.normalized * 5f;
                yield return new WaitForSeconds(0.5f);
                _rigid.velocity = Vector2.zero;
                yield return new WaitForSeconds(2f);
            }
        }

        protected override void DetectPlayer()
        {
        }
        
    }
}