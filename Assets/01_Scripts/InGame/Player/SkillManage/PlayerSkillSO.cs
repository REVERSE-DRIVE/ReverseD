using UnityEngine;

namespace SkillManage
{
    [CreateAssetMenu(menuName = "SO/")]
    public class PlayerSkillSO
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

        public PlayerSkill skillHandle;
        // 스킬을 인게임에 적용하는 방식을 어떻개 해야할지 고민임

        public void LevelUp(int amount)
        {
            skillLevel += amount;
        }

        public PlayerSkill GetSkill
        {
            get
            {
                skillHandle.SetSkillValue(
                    skillDuration,
                    coolTime,
                    minUseCoolTime);
                return skillHandle;
            }
        }
    }
}