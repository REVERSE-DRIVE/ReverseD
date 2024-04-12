using UnityEngine;

namespace EnemyManage.EnemyBossBase
{
    public class BossAVGIdleState : BossAVGState
    {
        private bool isDetectedPlayer = false;
        private Transform _playerTrm;
        private LayerMask _playerLayer;
        
        public BossAVGIdleState(Enemy enemyBase, EnemyStateMachine<BossAVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _playerTrm = GameManager.Instance._PlayerTransform;
            _playerLayer = LayerMask.GetMask("Player");
        }

        public override void UpdateState()
        {
            base.UpdateState();
            DetectPlayer();
        }

        public override void Exit()
        {
            base.Exit();
            isDetectedPlayer = false;
        }

        private void DetectPlayer()
        {
            Vector2 pos = _bossAVGBase.transform.position;
            Vector2 direction = (Vector2)_playerTrm.position - pos;
            RaycastHit2D hit = Physics2D.Raycast(pos, direction.normalized, 10, _playerLayer);
            if (hit.collider != null) 
            {
                SetRandomState();
            }        
        }

        private void SetRandomState()
        {
            int randomStateIndex = Random.Range(0, _bossAVGBase._randomPickState.Length);
            _stateMachine.ChangeState(_bossAVGBase._randomPickState[randomStateIndex], true);
        }
    }
}