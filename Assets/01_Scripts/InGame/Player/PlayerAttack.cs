using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected Collider2D[] _attackColliders;
    protected SpriteRenderer[] _attackSpriteRenderers;
    protected Animator[] _attackAnimators;
    protected bool isAllowAttack = true;
    [SerializeField] protected float attackTime;

    private void Awake()
    {
        _attackColliders = GetComponentsInChildren<Collider2D>();
        _attackSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _attackAnimators = GetComponentsInChildren<Animator>();
    }

    public virtual void Attack()
    {
        Debug.Log("PlayerAttack");
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
