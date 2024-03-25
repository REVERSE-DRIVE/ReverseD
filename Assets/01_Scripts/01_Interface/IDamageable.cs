using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace entityManage
{
    
    public interface IDamageable
    {
        
        public void Damaged(int damage);

        public void CriticalDamaged(int damage);

        public void Die();
    }
}
