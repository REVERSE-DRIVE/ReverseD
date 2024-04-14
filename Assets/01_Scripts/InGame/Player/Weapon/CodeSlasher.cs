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
            TakeDamageToTargets(DetectEnemy());
            TakeDamageToTargets(DetectFieldObject());
        }
    }
}