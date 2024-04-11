using System;
using UnityEngine;


namespace EnemyManage
{
    public class EnemyState<T> where T : Enum
    {
        protected EnemyStateMachine<T> _stateMachine;
        protected Enemy _enemyBase;
        protected bool _endTriggerCalled;
        protected string _animBoolParam;

        public EnemyState(Enemy enemyBase, EnemyStateMachine<T> stateMachine, string animBoolName)
        {
            _enemyBase = enemyBase;
            _stateMachine = stateMachine;
            _animBoolParam = animBoolName;
        
        }

        public virtual void UpdateState()
        {
        
        
        }

        public virtual void Enter()
        {
        
        
            _endTriggerCalled = false;
            _enemyBase.AnimatorCompo.SetBool(_animBoolParam, true);
        
        }

        public virtual void Exit()
        {
            _enemyBase.AnimatorCompo.SetBool(_animBoolParam, false);

        }

        public void AnimationFinishTrigger()
        {
            _endTriggerCalled = true;
        }

    
    }

}