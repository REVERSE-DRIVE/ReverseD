using UnityEngine;

namespace SkillManage
{
    [CreateAssetMenu(menuName = "SO/PlayerSkill")]
    public abstract class PlayerSkill : ScriptableObject
    {
        public string skillName;
        public string description;
        public int skillLevel;

        public bool isPassive;
        public float skillDuration;
        public float coolTime;
        /**
         * 스킬의 다음 사용까지의 최소 시간
         */
        public float minUseCoolTime;

        private Player _playerBase;
        
        public void LevelUp(int amount)
        {
            skillLevel += amount;
        }

        public void SetSkillValue(Player player)
        {
            _playerBase = player;
        }

        public virtual void ActiveSkill()
        {
            
        }


        public abstract void UpdateSkill();


    }
}