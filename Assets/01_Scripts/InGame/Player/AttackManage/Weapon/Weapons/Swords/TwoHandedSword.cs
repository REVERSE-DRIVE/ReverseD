using UnityEngine;

namespace AttackManage
{
    public abstract class TwoHandedSword : Sword
    {
        protected override Collider2D[] DetectTargets()
        {
            _DetectCenterPos = (Vector2)transform.position;
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(_DetectCenterPos, _attackRadius, _whatIsTarget);
            
            return hitTargets;
        }
    }
}