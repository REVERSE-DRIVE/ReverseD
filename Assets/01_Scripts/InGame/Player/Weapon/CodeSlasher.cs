using UnityEngine;

namespace AttackManage
{
    public class CodeSlasher : Sword
    {
        public override void Attack()
        {
            print("공격 시작");
            AttackAnimationOnTrigger();
        }

        public override void AttackEnd()
        {
            print("공격 끝");
            AttackAnimationOffTrigger();
        }
    }
}