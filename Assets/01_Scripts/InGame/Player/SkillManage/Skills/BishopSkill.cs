﻿using UnityEngine;

namespace SkillManage
{
    [CreateAssetMenu(menuName = "SO/PlayerSkill/Bishop")]
    public class BishopSkill : PlayerSkill
    {
        private float _currentTime = 0;
        public int healAmount;


        public override void ActiveSkill()
        {
            Player.OnPlayerHpChangedEvent += Heal;
        }

        public override void UpdateSkill()
        {
            _currentTime += Time.deltaTime * TimeManager.TimeScale;
            if (_currentTime >= 1)
            {
                _currentTime = 0;
                Heal();
            }
            
        }
        public override void EndSkill()
        {
            Player.OnPlayerHpChangedEvent -= Heal;
        }
        
        private void Heal()
        {
            
            _playerBase.RestoreHealth(healAmount * skillLevel);
        }
    }
}