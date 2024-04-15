using EnemyManage;
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

        _currentLifeTime += Time.deltaTime * TimeManager.TimeScale;

        if (_currentLifeTime >= lifeTime)
        {
            
            DestroyProjectile();
            return;
        }
        Move();
    }

    public virtual void Fire(Vector2 pos, Vector2 direction)
    {
        SetDefault();
        transform.position = pos;
        Fire(direction);
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
        _rigid.velocity = _direction.normalized * _speed * TimeManager.TimeScale; 
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
    }

    protected virtual void Hit(Collider2D other)
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
                    DamageToPlayer(other.GetComponent<Player>());
                }
                break;
            
            case ProjectileTarget.Enemy:

                if (other.CompareTag("Enemy"))
                {
                    DamageToEnemy(other.GetComponent<Enemy>());
                }
                break;
        }
    }

    protected virtual void DamageToPlayer(Player player)
    {
        player.TakeDamage(_damage);
        DestroyProjectile();
    }

    protected virtual void DamageToEnemy(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
        DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        PoolManager.Get(_destroyParticlePrefab, transform.position, Quaternion.identity);
        
        PoolManager.Release(gameObject);

    }
}