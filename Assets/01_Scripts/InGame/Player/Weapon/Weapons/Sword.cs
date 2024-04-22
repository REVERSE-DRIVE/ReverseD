﻿using System.Collections;
using UnityEngine;

namespace AttackManage
{
    public abstract class Sword : Weapon
    {
        [Header("Sword Setting")]
        [SerializeField] protected float _attackRadius = 2.5f;
        [SerializeField] protected bool _isSplash = true;
        [SerializeField] protected float _damageTiming;
        
        // 실질적인 Attack 기능을 하위 무기에서 구현

        /**
         * <summary>
         * 실질적으로 데미지를 적과 타겟에게 입히는 타이밍을 구현
         * </summary>
         */
        protected abstract IEnumerator AttackCoroutine();

    }
}