using System;

namespace EffectManage
{
    
    public abstract class Effect<T> where T : Enum
    {
        public T type;
        protected Entity _entityBase;
        protected int _leftTime = 1;
        protected int _level;

        public int effectLevel => _level;

        public void SetLevel(int value)
        {
            _level = value;
            LevelFixed();
        }
        
        public Effect(Entity entityBase, int level, int duration)
        {
            _entityBase = entityBase;
            _level = level;
            _leftTime = duration;
        }

        /**
         * <summary>
         * 버프가 시작되었을때 실행되는 함수
         * </summary>
         */
        public abstract void EnterEffect();

        /**
         * <summary>
         * 이펙트 레벨이 변경(증가)되었을때 실행되는 함수
         * </summary>
         */
        public abstract void LevelFixed();
    
        // 안씀
        public virtual void UpdateEffect()
        {
        
        }

        /**
         * <summary>
         * Player에서 1초마다 호출해주는 함수
         * </summary>
         * <returns>
         * 해당 버프가 끝났는지를 bool로 반환
         * </returns>
         */
        public virtual bool UpdateEffectEverySecond()
        {
            _leftTime--;
            if (_leftTime <= 0) return true;
            EffectFunc();
            return false;

        }
        /**
         * <summary>
         * UpdateEffectEverySecond 함수를 통해 1초마다 실행되는 함수
         * </summary>
         */
        protected abstract void EffectFunc();

        /**
         * <summary>
         * 버프가 풀렸을때 실행할 함수
         * </summary>
         */
        public abstract void ExitEffect();

        public void AddDuration(int amount)
        {
            _leftTime += amount;
        }

    }
}