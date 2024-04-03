using System;
using UnityEngine;

namespace AttackManage
{
    
    public class PlayerAttackController : MonoBehaviour
    {

        [SerializeField] private WeaponSO _currentWeaponSO;

        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Transform weaponHandleTrm;
        public event Action onAttackEvent;
        
        protected Vector2 _playerTransform;
        protected Vector2 dir;
    
        [SerializeField] protected float attackTime;
        [SerializeField] protected LayerMask _whatIsEnemy;

        
        private void Awake()
        {
            
        }

        public void Attack()
        {
            
        }

        public void ChangeWeapon(WeaponSO weaponSO)
        {
            _currentWeaponSO = weaponSO;
            
            foreach (Transform child in weaponHandleTrm)
            {
                
                // 무기 아이템 버리는 코드 필요
                Destroy(child.gameObject);
            }
            SetWeaponOnHandle();
            
        }

        private void SetWeaponOnHandle()
        {
             _currentWeapon = Instantiate(_currentWeaponSO.weaponPrefab, weaponHandleTrm);
        }
        
    }
}