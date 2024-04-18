using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public class CodeSlasher : Sword
    {
        public override void Attack()
        {
            print("공격 시작");
            AttackAnimationOnTrigger();
            StartCoroutine(AttackCoroutine());
            
        }

        private IEnumerator AttackCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
            TakeDamageToTargets(DetectTargets());
        }

        public override void AttackEnd()
        {
            print("공격 끝");
            AttackAnimationOffTrigger();
        }

        
    }
}