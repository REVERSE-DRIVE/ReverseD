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

    public bool IsDead => status.hp <= 0;
    public bool canMove => status.moveSpeed == 0;
    public float MoveSpeed => status.moveSpeed;
    
    public static event Action OnPlayerHpChanged;
    private SoundObject _soundObject;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    
    
    private void Awake()
    {
        PlayerManager.Instance.UpdateStat();
        ModifyStatus();
        
        _soundObject = GetComponent<SoundObject>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    /**
     * <summary>
     * 스탯 업데이트
     * </summary>
     */
    private void ModifyStatus()
    {
        
        PlayerManager.Instance.UpdateStat();
        
        status.hp = PlayerManager.Instance.setting_hp;
        status.hpMax = PlayerManager.Instance.setting_hp;
        // 스테이터스 불러올때 어케 불러올거임
        
        status.attackDamage = PlayerManager.Instance.setting_attackDamage;
        status.moveSpeed = PlayerManager.Instance.setting_moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        status.hp -= damage;
        OnPlayerHpChanged?.Invoke();
        _soundObject.PlayAudio(0);
        IsDie();
    }
    

    private void IsDie()
    {
        if (IsDead)
        {
            Die();
            // 게임 오버 실행
        }
    }

    public void Reviver()
    {
        status.hp = (status.hpMax / 2);
        SetObjective(true);
    }

    public void SetObjective(bool value)
    {
        _spriteRenderer.enabled = value;
        _collider.enabled = value;
    }

    public override void Die()
    {
        Debug.Log("PLayer Die => GameOver");
        SetObjective(false);
        status.moveSpeed = 0;
    }
}