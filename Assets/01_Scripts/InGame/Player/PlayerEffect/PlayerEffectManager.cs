using UnityEngine;

namespace EffectManage
{

    public class PlayerEffectManager : MonoBehaviour
    {
        private Player _player;
        public EffectManageMachine<PlayerEffectEnum> EffectMachine { get; private set; }
        [SerializeField] private bool isCancelEffect;

        [Header("EffectTypeClass Strings")]
        [SerializeField] private string frontString;
        [SerializeField] private string backString;

        private void Awake()
        {
            _player = GetComponent<Player>();
            EffectMachine = new
                EffectManageMachine<PlayerEffectEnum>();
        }

        private void Start()
        {
            EffectMachine.Initialize(_player, frontString, backString);
        }

        private void Update()
        {
            EffectMachine.UpdateEffect();
        }

        public void GetEffect(PlayerEffectEnum effectType, int level, int time)
        {
            EffectMachine.AddEffect(effectType, level, time);
        }
    }

}