using System;
using entityManage;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Status status;
    
    public Status Status { get; set; }
    
    private void Awake()
    {
        PlayerManager.Instance.UpdateStat();
        UpdateStatus();
    }

    private void Update()
    {
        if (status.hp <= 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    /**
     * <summary>
     * 스탯 업데이트
     * </summary>
     */
    public void UpdateStatus()
    {
        PlayerManager.Instance.UpdateStat();
        status.hp = PlayerManager.Instance.PlayerHealth;
        status.attackDamage = PlayerManager.Instance.AttackRange;
        status.moveSpeed = PlayerManager.Instance.Speed;
    }
}