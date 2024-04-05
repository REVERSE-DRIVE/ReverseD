namespace AttackManage
{
    public abstract class Sword : Weapon
    {
        // 실질적인 Attack 기능을 하위 무기에서 구현
        
        protected void DetectEnemy()
        {
            AttackAnimationOnTrigger();
            
            
        }
    }
}