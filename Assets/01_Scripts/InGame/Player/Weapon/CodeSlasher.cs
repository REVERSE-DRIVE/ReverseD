using EnemyManage;
using UnityEngine;

namespace AttackManage
{
    public class CodeSlasher : Sword
    {
        public override void Attack()
        {
            print("공격 시작");
            AttackAnimationOnTrigger();
            AttackRoutine();
        }

        public override void AttackEnd()
        {
            print("공격 끝");
            AttackAnimationOffTrigger();
        }

        private void AttackRoutine()
        {
            Collider2D[] hits = DetectEnemy();
            if (hits == null) return;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (isKnockBack)
                    {
                        enemy.TakeDamageWithKnockBack(
                            damage, GameManager.Instance._PlayerTransform.position,
                            knockBackPower);
                        continue;
                    }
                    enemy.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Enemy에게 Enemy 스크립트가 없습니다");
                }
                    
            }
        }
    }
}