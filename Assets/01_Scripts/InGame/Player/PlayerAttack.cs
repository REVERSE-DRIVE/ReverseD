using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected Animator[] _attackAnimators;
    protected bool isAllowAttack = true;
    protected PlayerController _playerController;
    protected Vector2 _playerTransform;
    protected Vector2 dir;
    protected float angle;
    
    [SerializeField] protected float attackTime;
    [SerializeField] protected LayerMask _whatIsEnemy;

    protected float AttakCooltime
    {
        get
        {
            return 5-(PlayerManager.Instance.AttackSpeed) * 0.5f;
        
        }
    }

    protected bool isCooltimeSet;
    protected float _attackCooltime = 0;
    
    private Quaternion _saveRotation;

    private void Awake()
    {
        _attackAnimators = GetComponentsInChildren<Animator>();
        _playerController = transform.parent.GetComponent<PlayerController>();
    }

    public virtual void Update()
    {
        Vector2 inputVec = _playerController.Joystick.Direction.normalized;
        if (inputVec.sqrMagnitude != 0)
        {
            dir = inputVec;

        }
        _playerTransform = transform.position;
        
        WeaponRotate();
    }
    
    private void WeaponRotate()
    {
        if (dir.sqrMagnitude == 0)
            return;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        if (Mathf.Abs(transform.rotation.z) > 0.7f)
        {
            transform.localScale = new Vector2(1, -1);
        }
        else
        {
            transform.localScale = Vector2.one;
        }
    }
    
    

    public virtual void Attack()
    {
        Debug.Log("PlayerAttack");
        if (!isCooltimeSet)
        {
            _attackCooltime = AttakCooltime;
        }
        if (!isAllowAttack) return;
        isAllowAttack = false;
        StartCoroutine(AttackRoutine());
    }

    public abstract IEnumerator AttackRoutine();

}
