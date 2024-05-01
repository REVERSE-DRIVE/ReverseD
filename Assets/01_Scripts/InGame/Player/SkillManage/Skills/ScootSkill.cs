using System.Collections;
using EntityManage;
using UnityEngine;

namespace SkillManage
{
    
    [CreateAssetMenu(menuName = "SO/PlayerSkill/Scoot")]
    public class ScootSkill : PlayerSkill
    {
        public float dashSpeed = 10;
        private Vector2 _dashDirection;
        public int defaultDamage = 15;
        public int multipleDamage = 2;
            
        public LayerMask targetLayer;
        private int _skillLayer = 17;
        private int _defaultPlayerLayer = 14;

        public int totalDamage => (defaultDamage + multipleDamage * skillLevel);
        
        public override void ActiveSkill()
        {
            _dashDirection = _playerController.GetInputVec;
            _playerController.rigid.velocity = _dashDirection.normalized * dashSpeed;
            _playerBase.gameObject.layer = _skillLayer;
        }

        public override void UpdateSkill()
        {
            DashAttack();
        }

        public override void EndSkill()
        {
            _playerController.rigid.velocity = Vector2.zero;
            _playerBase.gameObject.layer = _defaultPlayerLayer;
        }

        private void DashAttack()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(_playerTrm.position, 0.5f, targetLayer);
            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out IDamageable health))
                {
                    health.TakeDamage(totalDamage);
                }
            }
        }

        private IEnumerator DashCoroutine()
        {   
            yield return new WaitForSeconds(skillDuration);
        }
    }
}
