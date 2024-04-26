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
        /**
         * <summary>
         * 공격 지속시간
         * </summary>
         */
        public float attackTime = 1;
        public float attackRange = 1;
        public bool isKnockBack = false;
        public float knockBackPower = 1;
        public bool useAutoAiming = false;
        public float autoAimingDistance = 6;
        
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
            weaponPrefab._attackTime = attackTime;
            weaponPrefab._isKnockBack = isKnockBack;
            weaponPrefab._knockBackPower = knockBackPower;
            weaponPrefab._useAutoAiming = useAutoAiming;
            weaponPrefab._autoAimingDistance = autoAimingDistance;
            if (weaponPrefab is Sword)
            {
                Sword sword = (weaponPrefab as Sword);
                sword._attackRadius = attackRange;
            }
        }
        
        
    }
}