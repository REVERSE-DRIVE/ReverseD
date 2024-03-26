using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Setting Values")]
    [SerializeField] protected ProjectileType _projectileType;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected ProjectileTarget _projectileTarget;

    protected Vector2 _direction;

    [Header("State Values")]
    [SerializeField] protected bool isShot;

    protected Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }


    public virtual void Fire(Vector2 direction)
    {
        SetDefault();
        
        _direction = direction;
        isShot = true;
        Move();
    }

    protected virtual void Move()
    {
        _rigid.velocity = _direction.normalized * _speed; 
        SetRotation();
    }

    protected virtual void SetRotation()
    {
        transform.right = _direction.normalized;
    }

    // protected IEnumerator MoveRoutine()
    // {
    //     while (isShot)
    //     {
    //         
    //     }
    // }

    public virtual void SetDefault()
    {
        gameObject.SetActive(true);
        _rigid.velocity = Vector2.zero;
        _direction = Vector2.zero;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        switch (_projectileTarget)
        {
            case ProjectileTarget.Player:
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<Player>().TakeDamage(_damage);
                    PoolManager.Release(gameObject);
                }
                break;
            
            case ProjectileTarget.Enemy:

                break;
            
            default:
                Debug.Log($"[!] Projectile Target Missing (at {gameObject.name})");
                break;
        }
    }
}