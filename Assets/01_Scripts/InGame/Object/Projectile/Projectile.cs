using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileType _projectileType;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;

    protected Vector2 _direction;


    public virtual void Fire(Vector2 direction)
    {
        SetDefault();
    }

    protected virtual void Move()
    {
        
    }

    public virtual void SetDefault()
    {
        gameObject.SetActive(true);
        _direction = Vector2.zero;
    }
}