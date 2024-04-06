using System;
using EntityManage;
using UnityEngine;

namespace EnemyManage
{
    
    public class Enemy : Entity
    {

        [SerializeField] private ItemDropType ItemDropType;
        // ItemDropManager 에서 어떤 아이템을 드롭할지 Enum으로 호출함
        public event Action OnHealthChanged;


        private void Start()
        {
            OnHealthChanged += CheckIsDie;
        }

        /**
         * <summary>
         * EnemyInfo에서 세팅해주는 스테이터스 기본값
         * </summary>
         */
        [SerializeField]
        internal Status defaultStatus;
        //
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

        public override void Die()
        {
            ItemDropManager.Instance.DropItem(ItemDropType, transform.position);
            PoolManager.Release(gameObject);
            transform.SetParent(GameManager.Instance.DefaultEnemyParentTrm);
        }

        internal void SetHealthMax()
        {
            status.hp = status.hpMax;
        }

        public void SetStatusDefault()
        {
            status = defaultStatus;

        }


    }
}
