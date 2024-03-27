﻿using System;
using EntityManage;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    public Status Status
    {
        get { return status; }
        private set { }
    }
    public bool isDead;
    
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
        status.hp = PlayerManager.Instance.PlayerHealth;
        status.attackDamage = PlayerManager.Instance.AttackRange;
        status.moveSpeed = PlayerManager.Instance.Speed;
    }

    private void ModifyStatus()
    {
        
    }

    public void TakeDamage(int damage)
    {
        status.hp -= damage;
    }

    private void IsDie()
    {
        if (status.hp <= 0)
        {
            isDead = true;
            // 게임 오버 실행
        }
    }

    public override void Die()
    {
        throw new NotImplementedException();
    }
}