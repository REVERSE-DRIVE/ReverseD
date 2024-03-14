using UnityEngine;

namespace EnemyManage
{
    public class WaitingPattern : EnemyAttackPattern
    {
        private Vector2 movementDirection;
        
        public override void Enter()
        {
            
        }

        public override void Update()
        {
            
        }
        
        public override void Exit()
        {
            state = EnemyStateEnum.Roaming;
        }

        private void SetDirection()
        {
        }
    }
}