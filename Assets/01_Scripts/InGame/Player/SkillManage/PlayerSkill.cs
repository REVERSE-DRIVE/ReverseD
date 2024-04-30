using UnityEngine;

namespace SkillManage
{
    
    public abstract class PlayerSkill : ScriptableObject
    {
        [Header("Display Setting")]
        public string skillName = "Skill Name";
        public string description;
        public int skillLevel;
        public Sprite skillIcon;
        
        [Space(10)]
        [Header("Skill Settings")]
        public bool isPassive;
        public float skillDuration;
        public float coolTime;
        /**
         * 스킬의 다음 사용까지의 최소 시간
         */
        public float minUseCoolTime;

        protected Player _playerBase;
        
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