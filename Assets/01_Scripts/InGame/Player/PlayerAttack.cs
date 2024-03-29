using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected Transform[] _attackColliders;
    protected bool isAllowAttack = true;
    [SerializeField] protected float attackTime;

    private void Awake()
    {
        _attackColliders = GetComponentsInChildren<Transform>();
    }

    public virtual void Attack()
    {
        if (!isAllowAttack) return;
        isAllowAttack = false;
        for (int i = 1; i < _attackColliders.Length; i++)
        {
            _attackColliders[i].gameObject.SetActive(false);
        }
        StartCoroutine(AttackRoutine());
    }

    public abstract IEnumerator AttackRoutine();
}
