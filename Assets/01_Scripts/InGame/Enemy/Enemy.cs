using System;
using System.Collections;
using EntityManage;
using UnityEngine;

namespace EnemyManage
{
    
    public abstract class Enemy : Entity
    {

        [SerializeField] protected ItemDropType ItemDropType;
        // ItemDropManager 에서 어떤 아이템을 드롭할지 Enum으로 호출함
        public event Action OnHealthChanged;
        protected Rigidbody2D _rigid;

        protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Material _hitMaterial;
        protected Material _defaultMaterial;
        public bool CanStateChangeable { get; set; }
        public Animator AnimatorCompo;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _defaultMaterial = _spriteRenderer.material;
            if(AnimatorCompo == null)
                AnimatorCompo = GetComponent<Animator>();
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

        public virtual void AnimationEndTrigger()
        {
            
        }
    }
}
