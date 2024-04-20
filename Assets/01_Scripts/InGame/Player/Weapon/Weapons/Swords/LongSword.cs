using System.Linq;
using UnityEngine;

namespace AttackManage
{
    public abstract class LongSword : Sword
    {
        protected override Collider2D[] DetectTargets()
        {
            _DetectCenterPos = (Vector2)transform.position + (attackDirection * _attackRadius);
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(_DetectCenterPos, _attackRadius, _whatIsTarget);
            
            return hitTargets;
        }

        
    }
}