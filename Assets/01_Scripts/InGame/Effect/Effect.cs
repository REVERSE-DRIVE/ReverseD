using System;

namespace EffectManage
{
    
    public abstract class Effect<T> where T : Enum
    {
        public T type;
        protected Entity _entityBase;
        protected int _leftTime = 1;
        
        public Effect(Entity entityBase, int duration)
        {
            _entityBase = entityBase;
            _leftTime = duration;
        }

        public abstract void EnterEffect();
    
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

        public abstract void ExitEffect();

        public void AddDuration(int amount)
        {
            _leftTime += amount;
        }

        protected abstract void EffectFunc();
    }
}