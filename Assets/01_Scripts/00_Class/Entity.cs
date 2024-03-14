using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entityManage;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public Status status;


    public void Damage(int damage)
    {
        int _damage = (int)(damage * Random.Range(0.9f, 1.15f));
        if (Random.Range(0, 99) < status.criticalRate)
        {
            // 크리
            CriticalDamaged(_damage);
        }
        else
        {
            // 노크리
            Damaged(_damage);
        }
    }
    public void Damaged(int damage)
    {
        if (status.isHealDefense)
        {
            status.hp += (int)(status.healthDefMultiple * damage);
        }
        else
        {
            status.hp -= CalcDamage(damage, status.defense);
        }
        
    }

    public void CriticalDamaged(int damage)
    {
        status.hp -= CalcDamage((int)(damage * 1.5f), status.defense);

    }

    public int CalcDamage(int atk, int def)
    {
        return Mathf.Clamp(atk - def, 0, 999);
    }

}
