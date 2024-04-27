using UnityEngine;

namespace AttackManage
{
    public class MK2 : RifleGun
    {
        public override void AttackEnd()
        {
            
            
        }

        protected override void Update()
        {
            AutoAim();
            
        }

        public override void Fire()
        {
            FireProjectile(attackDirection);
        }

        public override void BulletLackEventHandler()
        {
            
            
        }
    }
}