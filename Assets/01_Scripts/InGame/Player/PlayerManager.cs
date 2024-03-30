using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int arrange;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int knockback;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private PlayerAttack playerAttack;

    private Player _player;

    public int setting_hp => playerSO.playerHealth;
    public int setting_moveSpeed => playerSO.moveSpeed;
    public int setting_arrange => arrange;
    public int setting_attackDamage => playerSO.attackDamage;
    public int AttackSpeed => attackSpeed;
    public int Knockback => knockback;
    public Sprite PlayerSprite => playerSprite;

    public PlayerAttack PlayerAttack
    {
        get => playerAttack;
        set => playerAttack = value;
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
    
    public WeaponType WeaponType
    {
        get => weaponType;
        set => weaponType = value;
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
        attackSpeed = playerSO.attackSpeed;
        knockback = playerSO.knockback;
        weaponType = playerSO.weaponType;
        playerSprite = playerSO.playerSprite;
        playerSO.SetCharacter();
    }

}
