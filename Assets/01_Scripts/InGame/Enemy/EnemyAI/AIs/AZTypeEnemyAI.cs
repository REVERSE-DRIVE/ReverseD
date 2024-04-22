using System.Collections;
using UnityEngine;

namespace EnemyManage.AIs
{
    public class AZTypeEnemyAI : EnemyAI
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _currentState;

        [Header("State1 Setting")] 
        [SerializeField] private float _chargeTime = 5f;
        private float _currentCharge = 0;

        [Header("State2 Setting")] 
        [SerializeField] private float _attackDistance = 10;
        [SerializeField] private float _playerTargetingTime = 2;
        [SerializeField] private Projectile _fallProjectile;
        [SerializeField] private int _stateRepeatAmount = 3;
        
        protected override void Attack()
        {
            throw new System.NotImplementedException();
        }
        
        

        private void AZState1()
        {
            
        }

        private void AZState2()
        {
            
        }

        private IEnumerator ChargeCoroutine()
        {
            
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator State2Coroutine()
        {
            int currentAttackAmount = 0;
            while (currentAttackAmount < _stateRepeatAmount)
            {
                
                yield return null;
            }
        }
        
    }
}