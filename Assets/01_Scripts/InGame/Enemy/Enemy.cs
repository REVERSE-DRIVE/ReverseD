using System;
using System.Collections;
using System.Collections.Generic;
using EntityManage;
using UnityEngine;

namespace EnemyManage
{
    
    public class Enemy : Entity
    {
        [SerializeField] private ItemDropType ItemDropType;
        // ItemDropManager 에서 어떤 아이템을 드롭할지 Enum으로 호출함

        private Status defaultStatus;

        private void Awake()
        {
            defaultStatus = status;
        }

        public override void Die()
        {
            PoolManager.Release(gameObject);
        }

    }
}
