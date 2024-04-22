using System.Linq;
using UnityEngine;

namespace AttackManage
{
    public abstract class DaggerSword : Sword
    {
        [Header("Dagger Setting")] [SerializeField]
        protected float _attackLength;
        protected override Collider2D[] DetectTargets()
        {
            Vector2 centerPos = (Vector2)transform.position + (attackDirection * _attackRadius);
            print(centerPos);
            print(_attackRadius);
            RaycastHit2D[] hitTargets = Physics2D.CircleCastAll(centerPos, _attackRadius, attackDirection, _attackLength, _whatIsTarget);

            Collider2D[] result = new Collider2D[hitTargets.Length]; 
            for (int i = 0; i < hitTargets.Length; i++)
            {
                result[i] = hitTargets[i].collider;
            }
            
            return result;
        }
    }
}