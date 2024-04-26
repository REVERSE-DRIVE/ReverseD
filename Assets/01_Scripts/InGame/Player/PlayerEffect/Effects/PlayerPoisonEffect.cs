namespace EffectManage.Effects
{
    
    public class PlayerPoisonEffect : Effect<PlayerEffectEnum>
    {
        public PlayerPoisonEffect(Entity entityBase, int level, int duration) : base(entityBase, level, duration)
        {
        }

        public override void EnterEffect()
        {
            throw new System.NotImplementedException();
        }

        public override void LevelFixed()
        {
            throw new System.NotImplementedException();
        }

        protected override void EffectFunc()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitEffect()
        {
            throw new System.NotImplementedException();
        }
    }
}
