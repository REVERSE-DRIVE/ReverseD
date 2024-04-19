using UnityEngine;

namespace AttackManage
{
    public abstract class Sword : Weapon
    {
        [Header("Sword Setting")]
        [SerializeField] protected float _attackRadius = 2.5f;
        [SerializeField] protected bool _isSplash = true;
        
        // 실질적인 Attack 기능을 하위 무기에서 구현

    }
}