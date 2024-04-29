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
    
    public static event Action OnPlayerHpChangedEvent;
    public event Action OnPlayerDieEvent;
    private SoundObject _soundObject;
    private Collider2D _collider;
    [SerializeField] private EffectObject _dieParticle;
    
    private void Awake()
    {
        PlayerManager.Instance.UpdateStat();
        ModifyStatus();
        
        _soundObject = GetComponent<SoundObject>();
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
        
        status.moveSpeed = PlayerManager.Instance.setting_moveSpeed;
    }

    public override void TakeDamage(int amount)
    {
        status.hp -= amount;
        OnPlayerHpChangedEvent?.Invoke();
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
        _collider.enabled = value;
        
    }

    public override void Die()
    {
        SetObjective(false);
        SetObjective(false);
        status.moveSpeed = 0;
        PoolManager.Get(_dieParticle, transform.position, Quaternion.identity);
        OnPlayerDieEvent?.Invoke();
    }
}