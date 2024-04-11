using System;
using System.Collections.Generic;

namespace EnemyManage
{
    public class EnemyStateMachine<T> where T : Enum
    {
        public EnemyState<T> CurrentState { get; private set; }
        public Dictionary<T, EnemyState<T>> StateDictionary 
            = new Dictionary<T, EnemyState<T>>();

        private Enemy _enemyBase;

        public void Initialize(T startState, Enemy enemy)
        {
            _enemyBase = enemy;
            CurrentState = StateDictionary[startState];
            CurrentState.Enter();

        }
    

        public void ChangeState(T newState, bool forceMode = false)
        {
            if (_enemyBase.CanStateChangeable == false && forceMode == false)
            {
                return;
            }

            if (_enemyBase.IsDie) return;
        
            CurrentState.Exit();
            CurrentState = StateDictionary[newState];
            CurrentState.Enter();
        
        
        }

        public void AddState(T stateEnum, EnemyState<T> state)
        {
            StateDictionary.Add(stateEnum, state);
        }
        
        
    }
}