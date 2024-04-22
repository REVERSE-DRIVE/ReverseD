using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public class CodeSlasher : LongSword
    {
        public override void AttackStart()
        {
            StartCoroutine(AttackCoroutine());

        }

        protected override IEnumerator AttackCoroutine()
        {
            yield return new WaitForSeconds(_damageTiming);
            ShowAttackEffect();
            TakeDamageToTargets(DetectTargets());
        }

        public override void AttackEnd()
        {
        }

        
    }
}