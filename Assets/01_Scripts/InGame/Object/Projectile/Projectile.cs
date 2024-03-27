using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Setting Values")]
    [SerializeField] protected ProjectileType _projectileType;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float lifeTime = 3f;
    [SerializeField] protected ProjectileTarget _projectileTarget;
    [SerializeField] private GameObject _destroyParticlePrefab;
    
    protected Vector2 _direction;
    protected float _currentLifeTime = 0;

    [Header("State Values")]
    [SerializeField] protected bool isShot;

    protected Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;

        if (_currentLifeTime >= lifeTime)
        {
            DestroyProjectile();
        }
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


    public virtual void SetDefault()
    {
        gameObject.SetActive(true);
        isShot = false;
        _currentLifeTime = 0;
        _rigid.velocity = Vector2.zero;
        _direction = Vector2.zero;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {   
            DestroyProjectile();
        }
        switch (_projectileTarget)
        {
            case ProjectileTarget.Player:
                if (other.CompareTag("Player"))
                {
                    
                    other.GetComponent<Player>().TakeDamage(_damage);
                    DestroyProjectile();
                }
                break;
            
            case ProjectileTarget.Enemy:

                if (other.CompareTag("Enemy"))
                {
                    
                    DestroyProjectile();
                }
                break;
            
        }
    }

    protected virtual void DestroyProjectile()
    {
        PoolManager.Get(_destroyParticlePrefab, transform.position, Quaternion.identity);
        
        PoolManager.Release(gameObject);

    }
}