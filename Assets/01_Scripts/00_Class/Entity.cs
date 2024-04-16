using UnityEngine;
using EntityManage;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [Header("Current Status")]
    [SerializeField]
    protected Status status;

    public Status Status
    {
        get
        {
            return status;
        }
        protected set { }
    }
    

    
    public bool IsDie
    {
        get
        {
            return status.hp <= 0;
        }
    }

    public void OnHealDefense()
    {
        status.isHealDefense = true;
    }

    public void OffHealDefense()
    {
        status.isHealDefense = false;
    }

    public void SetMoveSpeed(float value)
    {
        status.moveSpeed = value;
    }

    public virtual void Damage(int damage)
    {
        int _damage = (int)(damage * Random.Range(0.9f, 1.15f));
        if (Random.Range(0, 99) < status.criticalRate)
        {
            // 크리
            TakeCriticalDamage(_damage);
        }
        else
        {
            // 노크리
            TakeDamage(_damage);
        }
    }
    public virtual void TakeDamage(int damage)
    {
        if (status.isHealDefense)
        {
            status.hp += (int)(status.healthDefMultiple * damage);
        }
        else
        {
            status.hp -= CalcDamage(damage, status.defense);
        }

        ClampHealth();
    }

    public virtual void TakeCriticalDamage(int damage)
    {
        status.hp -= CalcDamage((int)(damage * 1.5f), status.defense);

    }
    public virtual void TakeStrongDamage(int amount)
    {
        TakeDamage(amount);
        
    }

    protected void CheckIsDie()
    {
        if (IsDie)
        {
            Die();
        }
    }

    public abstract void Die();
    public virtual void RestoreHealth(int amount)
    {
        status.hp += amount;
        ClampHealth();
    }

    public virtual int CalcDamage(int atk, int def)
    {
        return Mathf.Clamp(atk - def, 0, 999);
    }

    private void ClampHealth()
    {
        status.hp = Mathf.Clamp(status.hp, 0, status.hpMax);
    }
}
