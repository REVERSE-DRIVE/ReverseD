using entityManage;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerManager))]
public class Player : MonoBehaviour
{
    private Status status;
    private PlayerManager playerManager;
    
    public Status Status => status;
    public PlayerManager PlayerManager => playerManager;
    
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerManager.UpdateStat();
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        status.hp = playerManager.PlayerHealth;
        status.attackDamage = playerManager.AttackRange;
        status.moveSpeed = playerManager.Speed;
    }
}