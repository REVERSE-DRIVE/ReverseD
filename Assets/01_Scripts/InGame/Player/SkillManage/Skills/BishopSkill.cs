using UnityEngine;

namespace SkillManage
{
    public class BishopSkill: PlayerSkill
    {
        private float _currentTime = 0;
        public int healAmount;
        
        public override void UpdateSkill()
        {
            _currentTime += Time.deltaTime * TimeManager.TimeScale;
            if (_currentTime >= 1)
            {
                _currentTime = 0;
                Heal();
            }
            
        }

        private void Heal()
        {
                
            _playerBase.RestoreHealth(healAmount);
        }
        
    }
}