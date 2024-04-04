using System;
using EntityManage;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    public Status Status
    {
        get { return status; }
        private set { }
    }
    public bool isDead;
    public static event Action OnPlayerHpChanged;
    private SoundObject _soundObject;
    
    
    private void Awake()
    {
        PlayerManager.Instance.UpdateStat();
        UpdateStatus();
        _soundObject = GetComponent<SoundObject>();
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
        
        status.hp = PlayerManager.Instance.setting_hp;
        status.hpMax = PlayerManager.Instance.setting_hp;
        // 스테이터스 불러올때 어케 불러올거임
        
        status.attackDamage = PlayerManager.Instance.setting_attackDamage;
        status.moveSpeed = PlayerManager.Instance.setting_moveSpeed;
    }

    private void ModifyStatus()
    {
        
    }

    public void TakeDamage(int damage)
    {
        status.hp -= damage;
        OnPlayerHpChanged?.Invoke();
        _soundObject.PlayAudio(0);
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