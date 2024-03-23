using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage
{
    
    public abstract class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("공격 대상과 우선순위를 나타냄")]
        protected EnemyTargetingTaget[] _targetingObject;

        protected Enemy _enemy;
        [SerializeField] protected bool _isStatic;
        [SerializeField] protected EnemyStateEnum _currentState;

        protected Rigidbody2D _rigid;
        

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _enemy = GetComponent<Enemy>();
        }
        
        protected void Update()
        {
            Move();
        }



        protected virtual void DetectPlayer()
        {
            
        }

        protected virtual void Move()
        {
            
        }

        public virtual void SetDefault()
        {
            _currentState = EnemyStateEnum.Roaming;
        }
        
        
        protected abstract void Attack();
    }
}
