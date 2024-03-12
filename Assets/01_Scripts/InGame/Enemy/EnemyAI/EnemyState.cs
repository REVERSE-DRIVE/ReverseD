using UnityEngine;

namespace EnemyManage
{
    [System.Serializable]
    public abstract class EnemyState : ScriptableObject
    {
        [SerializeField] protected string StateName;
        public EnemyStateEnum state;
        

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