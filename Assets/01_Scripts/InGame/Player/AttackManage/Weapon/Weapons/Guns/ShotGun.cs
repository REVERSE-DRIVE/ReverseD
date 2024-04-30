using UnityEngine;

namespace AttackManage
{
    public abstract class ShotGun : Gun
    {
        [Header("ShotGun Setting")]
        [Range(3, 15)]
        [SerializeField] protected int _projectileAmount = 3;
        [SerializeField] protected float _projectileInterval = 0.1f;
        
        


    }
}