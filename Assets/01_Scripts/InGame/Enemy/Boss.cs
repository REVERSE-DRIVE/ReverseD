using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage
{
    
    public class Boss : Enemy
    {
        
        protected override void Awake()
        {
            base.Awake();
            
        }
        


        public override void Die()
        {
            base.Die();
            GameManager.Instance.Infect(15);
            
            
        }

        private void RefreshHealthGauge()
        {
            
        }
    }
}