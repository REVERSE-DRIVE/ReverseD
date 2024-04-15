using UnityEngine;

namespace EffectManage.Effects
{
    public class PlayerSlowEffect : Effect<PlayerEffectEnum>
    {

        private float _beforeValue;
            
        public PlayerSlowEffect(Entity entityBase, int level, int duration) : base(entityBase, level, duration)
        {
        }

        public override void EnterEffect()
        {
            //Debug.Log("기존 속도를 새로 정의함");
            _beforeValue = _entityBase.Status.moveSpeed;
            LevelFixed();
        }

        public override void LevelFixed()
        {
            float newSpeed = Mathf.Clamp(_beforeValue - (_beforeValue * 0.1f * _level), 0, 20);
            _entityBase.SetMoveSpeed(newSpeed);
        }

        protected override void EffectFunc()
        {
            
            
        }

        public override void ExitEffect()
        {
            //Debug.Log($"Slow해제, 기존 속도 {_beforeValue}로 설정");
            _entityBase.SetMoveSpeed(_beforeValue);
        }

    }
}