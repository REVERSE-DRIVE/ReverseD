using UnityEngine;

namespace AttackManage
{
    public abstract class Gun : Weapon
    {
        [Header("Gun Setting")] 
        [SerializeField] protected Transform _gunTip;
        [SerializeField] protected Projectile _projectile;
        [SerializeField] protected int _currentBullets;
        [SerializeField] protected int _needBullets = 1;
        [SerializeField] protected int _normalPickBullets = 10;
        [SerializeField] protected float _shotError = 0.3f;
        protected float _currentShotError = 0;
        
        public override void AttackStart()
        {
            if (_currentBullets >= _needBullets)
            {
                Fire();
                _currentBullets -= _needBullets;
            }
            else
            {
                BulletLackEventHandler();
            }
        }

        public abstract void Fire();

        public abstract void BulletLackEventHandler();

        public virtual void FillBullets()
        {
            FillBullets(_normalPickBullets);
        }
        public virtual void FillBullets(int amount)
        {
            _currentBullets += amount;
        }
    }
}