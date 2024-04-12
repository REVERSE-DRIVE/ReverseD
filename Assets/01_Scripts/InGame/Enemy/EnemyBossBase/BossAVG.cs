using System;
using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVG : Boss
    {
        public EnemyStateMachine<BossAVGStateEnum> StateMachine { get; private set; }

        [Header("Idle State Setting")] 
        [SerializeField] internal BossAVGStateEnum[] _randomPickState;

        [Header("Stun State Setting")] 
        [SerializeField] internal float _stunDuration = 10;

        [Header("Red State Setting")] 
        [SerializeField] private float _redStateDuration = 15f;
        [SerializeField] private Projectile _redProjectile1;
        [SerializeField] private Projectile _redProjectile2;
        
        
        
        [Header("Green State Setting")] 
        [SerializeField] internal float _greenStateDuration = 10f;
        //[SerializeField] private int _healMultiply = 3;
        [Header("Blue State Setting")] 
        [SerializeField] internal float _attacktime = 10f;
        [SerializeField] internal float _attackCooltime = 10f;
        [SerializeField] internal Projectile _projectile;
        [SerializeField] internal int _fireProjectileAmount = 4;
        [SerializeField] internal float _rotationSpeed = 3f;
        
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine<BossAVGStateEnum>();

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

        public void Attack()
        {
        }

        public override void AnimationEndTrigger()
        {
            StateMachine.CurrentState.AnimationFinishTrigger();
        }
    }
}