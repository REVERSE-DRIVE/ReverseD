using entityManage;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Status status;
    private PlayerManager playerManager;
    
    public Status Status => status;
    
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void UpdateStatus()
    {
        status.hp = playerManager.PlayerHealth;
        status.attackDamage = playerManager.AttackRange;
        status.moveSpeed = playerManager.Speed;
    }
}