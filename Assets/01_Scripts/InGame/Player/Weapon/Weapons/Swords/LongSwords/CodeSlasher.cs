using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public class CodeSlasher : LongSword
    {
        public override void Attack()
        {
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
            AttackAnimationOffTrigger();
        }

        
    }
}