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
        
        protected override void AutoAim()
        {
            Vector2 currentPosition = _gunTip.position;
            if (_useAutoAiming)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(currentPosition, _autoAimingDistance, _whatIsTarget); // 현재 위치 주변의 적을 검색

                Transform closestEnemy = null;
                float closestDistance = 99;

                // 모든 적에 대해 가장 가까운 적 찾기
                foreach (Collider2D enemyCollider in enemies)
                {
                    Vector2 direction = ((Vector2)enemyCollider.transform.position - currentPosition);
                    RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction.normalized, _autoAimingDistance, _wallLayer); // 레이 발사하여 장애물 감지

                    if (hit.collider == null) // 장애물이 없는 경우
                    {
                        if (direction.magnitude < closestDistance)
                        {
                            closestDistance = direction.magnitude;
                            closestEnemy = enemyCollider.transform;
                        }
                    }
                }

                if (closestEnemy != null)
                {
                    // 주어진 위치와 가장 가까운 적의 위치 사이의 방향 벡터 반환
                    
                    attackDirection = ((Vector2)closestEnemy.position - currentPosition).normalized;
                    WeaponRotateHandler(attackDirection);
                    _isAutoTargeted = true;
                }
                else
                {
                    // 가장 가까운 적이 없으면 기본 방향 벡터 반환 (예: 위쪽)
                    _isAutoTargeted = false;

                }

            }
        }

    }
}