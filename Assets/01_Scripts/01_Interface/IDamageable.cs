using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityManage
{
    
    public interface IDamageable
    {
        
        public void TakeDamage(int damage);

        public void TakeCriticalDamage(int damage);

        public void Die();
    }
}
