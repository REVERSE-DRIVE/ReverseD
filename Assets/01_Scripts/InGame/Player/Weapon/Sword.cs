﻿using UnityEngine;

namespace AttackManage
{
    public abstract class Sword : Weapon
    {
        [Header("Sword Setting")]
        [SerializeField] protected float _attackRadius = 1.5f;
        [SerializeField] protected bool _isSplash = true;
        
        // 실질적인 Attack 기능을 하위 무기에서 구현
        
        protected Collider2D[] DetectEnemy()
        {
            AttackAnimationOnTrigger();

            Vector2 centerPos = new Vector2(
                transform.position.x + _attackRadius * attackDirection.x,
                transform.position.y + _attackRadius * attackDirection.y
            );
            Collider2D[] hits = Physics2D.OverlapCircleAll(centerPos, _attackRadius, _whatIsEnemy);

            return hits;
        }
    }
}