using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace entityManage
{
    
    public interface IDamageable
    {
        
        public abstract void Damaged(Status status, int damage);

        public abstract void CriticalDamaged(Status Attacker, int damage);

    }
}
