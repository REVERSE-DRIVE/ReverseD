using UnityEngine;

namespace SkillManage
{
    public abstract class PlayerSkill
    {
        protected int _skillLevel;
        protected float _skillDuration;
        protected float _coolTime;
        /**
         * 스킬의 다음 사용까지의 최소 시간
         */
        protected float _minUseCoolTime;

        protected Player _playerBase;
        
        public float SkillDuration => _skillDuration;
        public float CoolTime => _coolTime;
        public float MinUseCoolTime => _minUseCoolTime;

        public void SetSkillValue(float duration, float coolTime, float minUseCoolTime)
        {
            _skillDuration = duration;
            _coolTime = coolTime;
            _minUseCoolTime = minUseCoolTime;
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