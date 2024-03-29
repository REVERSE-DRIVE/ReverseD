using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyManage
{
    
    public abstract class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        protected EnemyTargetingTaget[] _targetingObject;

        [SerializeField] protected bool _isStatic;
        [SerializeField] protected EnemyStateEnum _currentState;

        protected Rigidbody2D _rigid;
        

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
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
    }
}
