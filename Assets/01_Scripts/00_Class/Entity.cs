using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entityManage;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public Status status;


    public void Damaged(Status status, int damage)
    {
        throw new System.NotImplementedException();
    }

    public void CriticalDamaged(int damage)
    {
        throw new System.NotImplementedException();
    }
}
