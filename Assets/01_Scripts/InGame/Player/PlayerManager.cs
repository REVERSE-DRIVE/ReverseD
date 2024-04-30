using AttackManage;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private int arrange;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private PlayerAttackController _playerAttackController;

    private Transform _playerTrm;
    private Player _player;
    private PlayerSkillManager _skillManager;

    public int setting_hp => playerSO.playerHealth;
    public int setting_moveSpeed => playerSO.moveSpeed;
    
    public Sprite PlayerSprite => playerSprite;
    

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
        _playerTrm = _player.transform;
        _skillManager = _player.GetComponent<PlayerSkillManager>();
        
        
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
        
        _skillManager.ApplySkill(playerSO.playerSkill);
    }

    public void LoadSaveStatus()
    {
        // 저장된 스테이터스를 불러오는 함수를 구현해야함
    }

}
