using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected SpriteRenderer[] _attackSpriteRenderers;
    protected Animator[] _attackAnimators;
    protected bool isAllowAttack = true;
    protected PlayerController _playerController;
    protected Vector2 _playerTransform;
    protected Vector2 dir;
    
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask _whatIsEnemy;

    private void Awake()
    {
        _attackAnimators = GetComponentsInChildren<Animator>();
        _playerController = transform.parent.GetComponent<PlayerController>();
        _attackSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        dir = _playerController.Joystick.Direction;
        _playerTransform = transform.position;
    }

    public virtual void Attack()
    {
        Debug.Log("PlayerAttack");
        if (!isAllowAttack) return;
        isAllowAttack = false;
        StartCoroutine(AttackRoutine());
    }

    public abstract IEnumerator AttackRoutine();
}
