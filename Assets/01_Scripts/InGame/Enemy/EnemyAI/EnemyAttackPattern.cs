using UnityEngine;

namespace EnemyManage
{
    [System.Serializable]
    public abstract class EnemyAttackPattern
    {
        [SerializeField] protected string StateName;
        public EnemyStateEnum state;
        protected Transform _enemyTrs;

        public virtual void Enter()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Exit()
        {
            
        }
    }
}