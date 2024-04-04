using UnityEngine;

namespace AttackManage
{
    [CreateAssetMenu(menuName = "SO/Weapon/WeaponSO")]
    public class WeaponSO: ScriptableObject
    {
        public int id;
        public Rank rank = Rank.D;

        public WeaponType weaponType;
        public string weaponName;
        public string description;
        
        public int damage = 3;
        public float attackCooltime = 1;
        public float attackRange = 1;

        /**
         * <summary>
         * 직접 가져오기 보다는
         * </summary>
         */
        public Weapon weaponPrefab;

        public Weapon GetWeaponPrefab
        {
            get
            {
                SetWeaponValue();
                return weaponPrefab;
            }
        }
        
        public void SetWeaponValue()
        {
            weaponPrefab.damage = damage;
            weaponPrefab._attackCooltime = attackCooltime;
            
        }
        
        
    }
}