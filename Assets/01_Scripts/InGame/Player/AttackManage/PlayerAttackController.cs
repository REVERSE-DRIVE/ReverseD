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
        public event Action<Vector2> OnMoveDirectionEvent;

        private Player _player;
        private PlayerController _playerController;
        private Vector2 _playerTransform;
        private Vector2 _direction;
        [SerializeField] private bool _canAttack = true;

        [SerializeField] private float currentAttackTime = 0;

        private bool _useAutoAimCashing;

        public bool IsAttackCooldowned => currentAttackTime >= _currentWeapon._attackCooltime;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            SetWeaponOnHandle();
            _player.OnPlayerDieEvent += HandlePlayerDie;
        }


        private void Update()
        {
            if (TimeManager.TimeScale == 0) return;

            currentAttackTime += Time.deltaTime * TimeManager.TimeScale;
            _direction = _playerController.GetInputVec;

            Aiming();
        }

        private void Aiming()
        {
            if (_useAutoAimCashing && _currentWeapon._isAutoTargeted)
            {
                return;
            }
            if (_direction.sqrMagnitude != 0 && IsAttackCooldowned)
            {
                OnMoveDirectionEvent?.Invoke(_direction);
                
            }
        }


        // 추후 공격버튼 홀드를 통한 AnimatorTriggerOn/OffTrigger
        // 을 구현해야한다
        public void Attack()
        {
            if (IsAttackCooldowned && _canAttack)
            {
                //currentAttackTime >= _currentWeapon._attackCooltime
                currentAttackTime = 0;
                StartCoroutine(AttackRoutine());
            }
        }

        private IEnumerator AttackRoutine()
        {
            _currentWeapon.AttackStart();
            _currentWeapon.AttackAnimationOnTrigger();
            yield return new WaitForSeconds(_currentWeapon._attackTime);
            _currentWeapon.AttackAnimationOffTrigger();
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
            _useAutoAimCashing = _currentWeapon._useAutoAiming;
            OnMoveDirectionEvent += _currentWeapon.WeaponRotateHandler;
            
        }

        public void SetCanAttack(bool value)
        {
            _canAttack = value;
        }
        
        private void HandlePlayerDie()
        {
            SetCanAttack(false);
            OnMoveDirectionEvent = null;
            weaponHandleTrm.gameObject.SetActive(false);
        }

        
    }
}