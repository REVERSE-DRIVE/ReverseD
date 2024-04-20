using UnityEngine;

namespace AttackManage
{
    public abstract class TwoHandedSword : Sword
    {
        
        protected override Collider2D[] DetectTargets()
        {
            Vector2 centerPos = (Vector2)transform.position;
            print(centerPos);
            print(_attackRadius);
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(centerPos, _attackRadius, _whatIsTarget);
            
            return hitTargets;
        }
    }
}