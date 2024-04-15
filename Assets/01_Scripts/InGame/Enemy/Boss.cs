using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage
{
    
    public abstract class Boss : Enemy
    {

        


        public override void Die()
        {
            base.Die();
            GameManager.Instance.Infect(15);
            
            
        }

        protected abstract void SetStateEnum();
        
        protected void RefreshHealthGauge()
        {
            
        }


    }
}