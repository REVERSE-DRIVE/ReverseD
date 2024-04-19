using System.Linq;
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
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(centerPos, _attackRadius, _whatIsEnemy);
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(centerPos, _attackRadius, _whatIsFieldObject);
            Collider2D[] result = hitEnemys.Concat(hitObjects).ToArray();
            
            return result;
        }
    }
}