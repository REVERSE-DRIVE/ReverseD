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
    protected float angle;
    
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask _whatIsEnemy;
    
    private Quaternion _saveRotation;

    private void Awake()
    {
        _attackAnimators = GetComponentsInChildren<Animator>();
        _playerController = transform.parent.GetComponent<PlayerController>();
        _attackSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {
        dir = _playerController.Joystick.Direction.normalized;
        _playerTransform = transform.position;
        
        WeaponRotate();
    }
    
    private void WeaponRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        Debug.Log(Mathf.Abs(transform.rotation.z));
        if (Mathf.Abs(transform.rotation.z) > 0.7f)
        {
            transform.localScale = new Vector2(1, -1);
            Debug.Log("Flip");
        }
        else
        {
            transform.localScale = Vector2.one;
            Debug.Log("Not Flip");
        }
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
