using System;
using System.Collections;
using System.Collections.Generic;
using EntityManage;
using UnityEngine;

namespace EnemyManage
{
    
    public class Enemy : Entity, IDamageable
    {

        [SerializeField] private ItemDropType ItemDropType;
        // ItemDropManager 에서 어떤 아이템을 드롭할지 Enum으로 호출함

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

        public override void Die()
        {
            PoolManager.Release(gameObject);
            transform.SetParent(GameManager.Instance.DefaultEnemyParentTrm);
        }

        internal void SetHealthMax()
        {
            status.hp = status.HpMax;
        }

        public void SetStatusDefault()
        {
            status = defaultStatus;

        }

    }
}
