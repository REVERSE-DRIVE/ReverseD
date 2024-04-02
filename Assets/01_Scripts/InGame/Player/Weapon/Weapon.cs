using UnityEngine;

namespace AttackManage
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage = 3;
        [SerializeField] private float _attackCooltime = 1f;
        
        public abstract void Attack();

        
    }
}