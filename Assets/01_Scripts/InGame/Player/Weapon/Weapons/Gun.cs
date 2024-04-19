using UnityEngine;

namespace AttackManage
{
    public abstract class Gun : Weapon
    {
        [Header("Gun Setting")] 
        [SerializeField] protected Transform _gunTip;
        [SerializeField] protected Projectile _projectile;
        
    }
}