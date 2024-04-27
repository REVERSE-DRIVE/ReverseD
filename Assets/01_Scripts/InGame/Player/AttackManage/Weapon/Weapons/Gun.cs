using System;
using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public abstract class Gun : Weapon
    {
        [Header("Gun Setting")] 
        [SerializeField] protected Transform _gunTip;
        [SerializeField] protected Projectile _projectile;
        [SerializeField] protected int _currentBullets;
        [SerializeField] protected int _maxBullets;
        [SerializeField] protected int _needBullets = 1;
        [SerializeField] protected int _normalPickBullets = 10;
        [SerializeField] protected int _pickBulletMultiple = 2;
        [SerializeField] protected float _shotError = 0.3f;
        protected float _currentShotError = 0;

        [SerializeField] private Transform _gunBulletGaugeTrm;
        [SerializeField] protected GameObject _gunFirelightObject;
        
        public override void AttackStart()
        {
            if (_currentBullets >= _needBullets)
            {
                StartCoroutine(GunFireCoroutine());
                Fire();
                _currentBullets -= _needBullets;
                RefreshGauge();

            }
            else
            {
                BulletLackEventHandler();
            }
        }

        protected virtual void Update()
        {
            AutoAim();
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
            RefreshGauge();
        }

        protected virtual void RefreshGauge()
        {
            float ratio = Mathf.Clamp01((float)_currentBullets / _maxBullets);
            Vector3 fillAmount = new Vector3(ratio, 1, 1);
            _gunBulletGaugeTrm.localScale = fillAmount;
        }

        protected virtual void FireProjectile(Vector2 direction)
        {
            Projectile projectile = PoolManager.Get(_projectile, _gunTip.position, Quaternion.identity);
            projectile.Fire(direction);
            
        }

        private IEnumerator GunFireCoroutine()
        {
            _gunFirelightObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _gunFirelightObject.SetActive(false);

        }

    }
}