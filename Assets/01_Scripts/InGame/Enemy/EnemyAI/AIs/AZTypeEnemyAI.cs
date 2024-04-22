using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace EnemyManage.AIs
{
    public enum AZTypeEnemyState
    {
        Idle,
        Waiting,
        State1,
        State2
    }
    public class AZTypeEnemyAI : EnemyAI
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AZTypeEnemyState _currentState;
        [SerializeField] private AZTypeEnemyState[] _canChangeState;

        [Header("Idle Setting")] [SerializeField]
        private float _idleWaitTime = 3f;
        [Header("State1 Setting")] 
        [SerializeField] private float _attackDistance = 10;
        private float _currentCharge = 0;

        [Header("State2 Setting")] 
        [SerializeField] private float _chargeTime = 5f;
        [SerializeField] private float _playerTargetingTime = 2;
        [SerializeField] private TimedBombObject _fallBomb;
        [SerializeField] private int _stateRepeatAmount = 3;
        [SerializeField] private Transform _targetingMark;

        private Vector2 _targetPosition;

        private float _currentIdleTime = 0;
        
        
        protected void Attack()
        {
            
        }

        protected void Update()
        {
            if (TimeManager.TimeScale == 0) return;
            
            SelectState();
            
        }

        private void SelectState()
        {
            if (_currentState == AZTypeEnemyState.Idle)
            {
                _currentIdleTime += Time.deltaTime * TimeManager.TimeScale;
                if (_currentIdleTime >= _idleWaitTime)
                {
                    _currentIdleTime = 0;
                    ChangeState();
                }
            }
        }

        private void ChangeState()
        {
            AZTypeEnemyState state = _canChangeState[Random.Range(0, _canChangeState.Length)];
            _currentState = state;
            switch (_currentState)
            {
                case AZTypeEnemyState.State1:
                    AZState1();
                    break;
                case AZTypeEnemyState.State2:
                    AZState2();
                    break;
            }

        }
        
        
        
        

        private void AZState1()
        {
            
        }

        private void AZState2()
        {
            StartCoroutine(State2Coroutine());
        }

        private IEnumerator ChargeCoroutine()
        {
            
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator State2Coroutine()
        {

            for (int i = 0; i < _stateRepeatAmount; i++)
            {
                float currentTime = 0;

                while (currentTime <= _chargeTime)
                {
                    currentTime += Time.deltaTime * TimeManager.TimeScale;
                    _targetPosition = _playerTrm.position;
                    _targetingMark.position = _targetPosition;
                    yield return null;

                }

                yield return new WaitForSeconds(_playerTargetingTime);
                TimedBombObject bomb = PoolManager.Get(_fallBomb, _targetPosition, Quaternion.identity);
                bomb.OnTrigger();



            }

            _currentState = AZTypeEnemyState.Idle;
        }
        
        
        
    }
}