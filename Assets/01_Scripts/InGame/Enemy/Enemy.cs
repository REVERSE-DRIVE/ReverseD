using System;
using System.Collections;
using EntityManage;
using UnityEngine;

namespace EnemyManage
{
    
    public abstract class Enemy : Entity
    {
        public event Action OnHealthChanged;
        public event Action OnDieEvent;
        
        [Header("Setting Values")]
        [SerializeField] protected ItemDropType ItemDropType;
        [SerializeField] protected Material _hitMaterial;
       
        protected Material _defaultMaterial;
        public bool CanStateChangeable { get; set; } = true;
        
        // Compo
        protected Rigidbody2D _rigid;
        protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected EffectObject _enemyDieEffectPrefab;
        public Animator AnimatorCompo;
        private bool _isDead;
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _defaultMaterial = _spriteRenderer.material;
            if(AnimatorCompo == null)
                AnimatorCompo = GetComponent<Animator>();
        }


        protected virtual void Start()
        {
            OnHealthChanged += CheckIsDie;
            OnHealthChanged += HandlerHitEvent;
        }

        /**
         * <summary>
         * EnemyInfo에서 세팅해주는 스테이터스 기본값
         * </summary>
         */
        internal Status defaultStatus;
        
        public virtual void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            OnHealthChanged?.Invoke();
        }
        
        public virtual void TakeDamageWithKnockBack(int amount, Vector2 damageOrigin, float knockBackPower)
        {
            base.TakeDamage(amount);
            Vector2 knockBackDirection = ((Vector2)transform.position - damageOrigin).normalized;
            _rigid.AddForce(knockBackDirection * knockBackPower, ForceMode2D.Impulse);
            OnHealthChanged?.Invoke();
        }

        public override void Die()
        {
            if (!_isDead)
            {
                _isDead = true;
                OnDieEvent?.Invoke();
                PoolManager.Get(_enemyDieEffectPrefab, transform.position, Quaternion.identity).Play();
                ItemDropManager.Instance.DropItem(ItemDropType, transform.position);
                StartCoroutine(DieRoutine());
            }
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

        public virtual void SetStatusDefault()
        {
            status = defaultStatus;
            _isDead = false;
        }

        protected virtual void HandlerHitEvent()
        {
            StartCoroutine(HitRoutine());
            
        }

        protected virtual IEnumerator HitRoutine()
        {
            _spriteRenderer.material = _hitMaterial;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = _defaultMaterial;
        }

        public abstract void AnimationEndTrigger();
    }
}
