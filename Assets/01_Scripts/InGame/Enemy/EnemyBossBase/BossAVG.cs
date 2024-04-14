using System;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVG : Boss
    {
        public EnemyStateMachine<BossAVGStateEnum> StateMachine { get; private set; }
        [SerializeField] internal SoundObject _soundObject;
        [Header("Idle State Setting")] 
        [SerializeField] internal BossAVGStateEnum[] _randomPickState;
        [SerializeField] internal float _idleWaitingTime = 5f;
        [Header("Stun State Setting")] 
        [SerializeField] internal float _stunDuration = 10;

        [Header("Red State Setting")] 
        [SerializeField] internal int _chargeEnergy = 20;
        [SerializeField] internal int _burstDamage = 100;
        [SerializeField] internal float _chargingSpeed = 2;
        [SerializeField] internal AVGStructureObject _structureObject;
        [SerializeField] internal ParticleSystem _chargingParticle;
        [SerializeField] internal ParticleSystem _burstParticle;
        [Header("Green State Setting")] 
        [SerializeField] internal float _greenStateDuration = 30f;
        [SerializeField] internal int _healCoreHealAmountPerSecond = 30;
        [SerializeField] internal AVGHealingObject[] _healingObjects;
        //[SerializeField] private int _healMultiply = 3;
        [Header("Blue State Setting")] 
        [SerializeField] internal float _attacktime = 10f;
        [SerializeField] internal float _attackCooltime = 10f;
        [SerializeField] internal Projectile _projectile;
        [SerializeField] internal int _fireProjectileAmount = 4;
        [SerializeField] internal float _rotationSpeed = 3f;

        [Header("Yellow State Setting")] 
        [SerializeField] internal float _yellowStateDuration = 30f;
        [SerializeField] internal bool _isResist;
        
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine<BossAVGStateEnum>();
            _soundObject = GetComponent<SoundObject>();
            //여기에 상태를 불러오는 코드가 필요하다.
            SetStateEnum();

        }
        
        protected override void SetStateEnum()
        {
            foreach (BossAVGStateEnum stateEnum in Enum.GetValues(typeof(BossAVGStateEnum)))
            {
                string typeName = stateEnum.ToString();
                Type t = Type.GetType($"EnemyManage.EnemyBossBase.BossAVG{typeName}State");
                
                try
                {
                    EnemyState<BossAVGStateEnum> state =
                        Activator.CreateInstance(t, this, StateMachine, $"State{typeName}") as EnemyState<BossAVGStateEnum>;
                    StateMachine.AddState(stateEnum, state);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Enemy Boss AVG : no State found [ {typeName} ] - {ex.Message}");
                }
            }
        }

        private void Start()
        {
            StateMachine.Initialize(BossAVGStateEnum.Idle, this);
        }

        private void Update()
        {
            StateMachine.CurrentState.UpdateState();
        }

        public void ForceStun()
        {
            StateMachine.ChangeState(BossAVGStateEnum.Stun, true);
        }

        public override void TakeDamage(int amount)
        {
            if (_isResist) return;
            base.TakeDamage(amount);
        }
        

        public void Attack()
        {
        }

        public override void AnimationEndTrigger()
        {
            StateMachine.CurrentState.AnimationFinishTrigger();
        }
    }
}