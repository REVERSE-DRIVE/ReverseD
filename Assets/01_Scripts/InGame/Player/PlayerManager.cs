using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int playerHealth;
    [SerializeField] private int attackRange;
    [SerializeField] private int speed;
    [SerializeField] private int arrange;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int knockback;
    [SerializeField] private Sprite playerSprite;

    
    public int PlayerHealth => playerHealth;
    public int AttackRange => attackRange;
    public int Speed => speed;
    public int Arrange => arrange;
    public int AttackSpeed => attackSpeed;
    public int Knockback => knockback;
    public Sprite PlayerSprite => playerSprite;
    
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
    

    public void UpdateStat()
    {
        playerHealth = playerSO.playerHealth;
        attackRange = playerSO.attackRange;
        speed = playerSO.speed;
        arrange = playerSO.arrange;
        attackSpeed = playerSO.attackSpeed;
        knockback = playerSO.knockback;
        weaponType = playerSO.weaponType;
        playerSprite = playerSO.playerSprite;
    }
}
