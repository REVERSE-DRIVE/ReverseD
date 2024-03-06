using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entityManage;

public class Entity : MonoBehaviour, IDamageable
{
    public Status status;

    public void Damaged(int hp)
    {
        throw new System.NotImplementedException();
    }
}
