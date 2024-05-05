using System.Collections;
using EnemyManage.EnemyBase;
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
        
        private NormalEnemy _normalEnemy;
        
        private readonly int IdleHash = Animator.StringToHash("Idle");
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private readonly int DieHash = Animator.StringToHash("Die");
        
        protected override void Start()
        {
            base.Start();
            _normalEnemy = GetComponent<NormalEnemy>();
            _normalEnemy.OnDieEvent += HandleDieEvent;
        }

        private void HandleDieEvent()
        {
            _normalEnemy.AnimatorCompo.SetBool(IdleHash, false);
            _normalEnemy.AnimatorCompo.SetBool(AttackHash, false);
            _normalEnemy.AnimatorCompo.SetBool(DieHash, true);
            
            SetDefault();
        }

        public override void SetDefault()
        {
            _currentState = AZTypeEnemyState.Idle;
            _targetingMark.gameObject.SetActive(false);
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
                _normalEnemy.AnimatorCompo.SetBool(IdleHash, true);
                _normalEnemy.AnimatorCompo.SetBool(AttackHash, false);
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
            _normalEnemy.AnimatorCompo.SetBool(IdleHash, false);
            _normalEnemy.AnimatorCompo.SetBool(AttackHash, true);
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