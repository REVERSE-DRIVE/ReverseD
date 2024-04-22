using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public class Grinder : TwoHandedSword
    {
        public override void AttackStart()
        {
            StartCoroutine(AttackCoroutine());
        }

        public override void AttackEnd()
        {
        }

        protected override IEnumerator AttackCoroutine()
        {
            yield return new WaitForSeconds(_damageTiming);
            ShowAttackEffect();
            TakeDamageToTargets(DetectTargets());
        }
        
        
    }
}