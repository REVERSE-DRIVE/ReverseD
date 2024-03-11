using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace entityManage
{
    
    public interface IDamageable
    {
        
        public abstract void Damaged(int damage);

        public abstract void CriticalDamaged(int damage);

    }
}
