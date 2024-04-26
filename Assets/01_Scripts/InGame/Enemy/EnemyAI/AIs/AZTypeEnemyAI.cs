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
        [SerializeField] private float _playerTargetingTime = 4;
        [SerializeField] private TimedBombObject _fallBomb;
        [SerializeField] private int _stateRepeatAmount = 3;
        [SerializeField] private Transform _targetingMark;
        [Range(0.05f, 0.5f)]
        [SerializeField] private float _followSpeed = 2;
        [SerializeField] private float _targetAccuracy = 3;
        [SerializeField] private float _attackTerm = 2;
        private Vector2 _targetPosition;

        private float _currentIdleTime = 0;
        
        
        public override void SetDefault()
        {
            _currentState = AZTypeEnemyState.Idle;
            _currentCharge = 0;
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
                Vector3 prevDirection = Vector3.zero;
                Vector2 dir;
                _targetingMark.position = transform.position;
                _targetingMark.gameObject.SetActive(true);
                while (currentTime <= _playerTargetingTime)
                {
                    currentTime += Time.deltaTime * TimeManager.TimeScale;
                    _targetPosition = _playerTrm.position;
                    dir = ((Vector3)_targetPosition - _targetingMark.position) * _targetAccuracy + prevDirection;
                    prevDirection = dir.normalized;
                    if (dir.magnitude >= 0.1f)
                    {
                        _targetingMark.Translate(dir.normalized * _followSpeed);
                    }
                    yield return null;
                }
                _targetingMark.gameObject.SetActive(false);
                
                TimedBombObject bomb = PoolManager.Get(_fallBomb, _targetingMark.position, Quaternion.identity);
                bomb.OnTrigger();
                yield return new WaitForSeconds(_attackTerm);
            }
            _currentState = AZTypeEnemyState.Idle;
        }

        
    }
}