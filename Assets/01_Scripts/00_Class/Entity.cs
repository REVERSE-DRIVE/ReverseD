using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entityManage;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public Status status;


    public void Damaged(Status Attacker, int damage)
    {
        status.hp -=Attacker.attackDamage - Attacker.defense;
    }

    public void CriticalDamaged(Status Attacker, int damage)
    {
        throw new System.NotImplementedException();
    }

    public int CalcDamage()
    {
        
    }

}
