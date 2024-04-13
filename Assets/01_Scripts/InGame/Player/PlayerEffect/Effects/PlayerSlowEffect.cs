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
            _beforeValue = _entityBase.Status.moveSpeed;
            LevelFixed();
        }

        public override void LevelFixed()
        {
            _entityBase.SetMoveSpeed(level);
        }

        protected override void EffectFunc()
        {
            
            
        }

        public override void ExitEffect()
        {
            _entityBase.SetMoveSpeed(_beforeValue);
        }

    }
}