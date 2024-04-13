using System;
using System.Collections.Generic;
using UnityEngine;

namespace EffectManage
{
    
    public class EffectManageMachine<T> where T : Enum
    {
        protected Entity _entityBase;
        public List<Effect<T>> effects = new List<Effect<T>>();

        protected string _frontString;
        protected string _backString;
        private bool isCancelEffect;
        protected float currentTime = 0;


        public void Initialize(Entity entityBase, string frontString, string backString)
        {
            _entityBase = entityBase;
            _frontString = frontString;
            _backString = backString;
        }
        
        /**
         * <summary>
         * Machine을 가지는 EffectManager의 내부 Update함수에서 실행시켜주어야함
         * </summary>
         */
        public virtual void UpdateEffect()
        {
            if (isCancelEffect) return;

            currentTime += Time.deltaTime;
            if (currentTime >= 1)
            {
                currentTime = 0;
                HandleCallEverySecond();

            }
        }

        protected virtual void HandleCallEverySecond()
        {
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].UpdateEffectEverySecond())
                {
                    effects[i].ExitEffect();
                    effects.Remove(effects[i]);
                    i--;
                }
            }

        }

        public virtual void AddEffect(T effectType, int level, int duration)
        {
            Effect<T> effect = IsHaveEffect(effectType);
            if (effect != null) 
            {
                Debug.Log($"이미 {effectType.ToString()}버프를 가지고 있음");
                if (effect.effectLevel < level)
                    effect.SetLevel(level);
                
                effect.AddDuration(duration);
                return;
            }
            Debug.Log($"새버프 {effectType.ToString()}버프를 가짐");
            string typeName = effectType.ToString();
            
            try
            {
                Type t = Type.GetType($"{_frontString}{typeName}{_backString}");
                Effect<T> state =
                    Activator.CreateInstance(t, _entityBase, level, duration) as Effect<T>;
                effects.Add(state);
                state.EnterEffect();
            }
            catch (Exception ex)
            {
                Debug.LogError($"{effectType.ToString()} : no Effect found [ {_frontString}{typeName}{_backString} ] - {ex.Message}");
            }
        }

        protected Effect<T> IsHaveEffect(T type)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].type.ToString() == type.ToString()) return effects[i];
            }
            return null;
        }
    }
}