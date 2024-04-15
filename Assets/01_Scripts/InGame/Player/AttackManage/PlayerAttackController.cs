using System;
using System.Collections;
using UnityEngine;

namespace AttackManage
{
    
    public class PlayerAttackController : MonoBehaviour
    {

        [SerializeField] private WeaponSO _currentWeaponSO;

        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Transform weaponHandleTrm;
        public event Action onAttackEvent;
        public event Action<Vector2> OnMoveDirectionEvent;


        private PlayerController _playerController;
        private Vector2 _playerTransform;
        private Vector2 dir;

        [SerializeField] protected float currentAttackTime = 0;
        [SerializeField] protected LayerMask _whatIsEnemy;

        
        
        public bool IsAttackCooldowned
        {
            get
            {
                return currentAttackTime >= _currentWeapon._attackCooltime;
            }
        }
        
        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            SetWeaponOnHandle();
        }

        private void Update()
        {
            if (TimeManager.TimeScale == 0) return;

            currentAttackTime += Time.deltaTime * TimeManager.TimeScale;
            dir = _playerController.GetInputVec;
            if (dir.sqrMagnitude != 0 && IsAttackCooldowned)
            {
                OnMoveDirectionEvent?.Invoke(dir);
                
            }
        }


        // 추후 공격버튼 홀드를 통한 AnimatorTriggerOn/OffTrigger
        // 을 구현해야한다
        public void Attack()
        {
            if (IsAttackCooldowned)
            {
                
                currentAttackTime = 0;
                StartCoroutine(AttackRoutine());
            }
        }

        private IEnumerator AttackRoutine()
        {
            _currentWeapon.Attack();
            yield return new WaitForSeconds(_currentWeapon._attackTime);
            _currentWeapon.AttackEnd();
        }

        public void ChangeWeapon(WeaponSO weaponSO)
        {
            OnMoveDirectionEvent = null;

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
             _currentWeapon = Instantiate(_currentWeaponSO.GetWeaponPrefab, weaponHandleTrm);
             OnMoveDirectionEvent += _currentWeapon.WeaponRotateHandler;
        }
        
    }
}