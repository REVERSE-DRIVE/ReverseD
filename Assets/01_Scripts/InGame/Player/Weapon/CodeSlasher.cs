namespace AttackManage
{
    public class CodeSlasher : Sword
    {
        public override void Attack()
        {
            AttackAnimationOnTrigger();
        }

        public override void AttackEnd()
        {
            AttackAnimationOffTrigger();
        }
    }
}