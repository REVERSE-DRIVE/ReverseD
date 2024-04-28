using AttackManage;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private int arrange;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private PlayerAttackController _playerAttackController;

    private Player _player;

    public int setting_hp => playerSO.playerHealth;
    public int setting_moveSpeed => playerSO.moveSpeed;
    public int setting_arrange => arrange;
    
    public Sprite PlayerSprite => playerSprite;
    
    //
    // public PlayerAttack PlayerAttack
    // {
    //     get => playerAttack;
    //     set => playerAttack = value;
    // }

    public PlayerAttackController playerAttackController
    {
        get => _playerAttackController;
        
    }
    
    public PlayerSO PlayerSO
    {
        get => playerSO;
        set
        {
            playerSO = value;
            UpdateStat();
        }
    }
    
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }


    /**
     * <summary>
     * 스탯 업데이트
     * </summary>
     */
    public void UpdateStat()
    {
        arrange = playerSO.arrange;
        playerSprite = playerSO.playerSprite;
        playerSO.SetCharacter();
    }

}
