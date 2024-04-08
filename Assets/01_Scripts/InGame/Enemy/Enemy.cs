using System;
using System.Collections;
using EntityManage;
using UnityEngine;

namespace EnemyManage
{
    
    public class Enemy : Entity
    {

        [SerializeField] private ItemDropType ItemDropType;
        // ItemDropManager 에서 어떤 아이템을 드롭할지 Enum으로 호출함
        public event Action OnHealthChanged;
        private Rigidbody2D _rigid;

        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Material _hitMaterial;
        private Material _defaultMaterial;

        protected void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _spriteRenderer.material;
        }


        protected void Start()
        {
            OnHealthChanged += CheckIsDie;
            OnHealthChanged += HitEventHandler;
        }

        /**
         * <summary>
         * EnemyInfo에서 세팅해주는 스테이터스 기본값
         * </summary>
         */
        internal Status defaultStatus;
        
        // private void OnEnable()
        // {
        //
        //     SetStatusDefault();
        // }
        
        public void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            OnHealthChanged?.Invoke();
        }
        
        public void TakeDamageWithKnockBack(int amount, Vector2 damageOrigin, float knockBackPower)
        {
            base.TakeDamage(amount);
            Vector2 knockBackDirection = ((Vector2)transform.position - damageOrigin).normalized;
            _rigid.AddForce(knockBackDirection * knockBackPower, ForceMode2D.Impulse);
            OnHealthChanged?.Invoke();
        }

        public override void Die()
        {
            ItemDropManager.Instance.DropItem(ItemDropType, transform.position);
            StartCoroutine(DieRoutine());
        }

        protected IEnumerator DieRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            transform.SetParent(GameManager.Instance.DefaultEnemyParentTrm);
            PoolManager.Release(gameObject);
            
        }

        internal void SetHealthMax()
        {
            status.hp = status.hpMax;
        }

        public void SetStatusDefault()
        {
            status = defaultStatus;

        }

        protected void HitEventHandler()
        {
            StartCoroutine(HitRoutine());
        }

        protected IEnumerator HitRoutine()
        {
            _spriteRenderer.material = _hitMaterial;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = _defaultMaterial;
        }

    }
}
