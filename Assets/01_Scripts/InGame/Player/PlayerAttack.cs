using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected Collider2D[] _attackColliders;
    protected bool isAllowAttack = true;

    private void Awake()
    {
        _attackColliders = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
    }

    public virtual void Attack()
    {
        if (!isAllowAttack) return;
        isAllowAttack = false;
        foreach (var collider in _attackColliders)
        {
            collider.enabled = false;
        }
        StartCoroutine(AttackRoutine());
    }

    public abstract IEnumerator AttackRoutine();
}
