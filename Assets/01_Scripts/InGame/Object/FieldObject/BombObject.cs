using Calculator;
using EntityManage;
using UnityEngine;

public class BombObject : FieldObject
{
    [Header("Bomb Setting")] [SerializeField]
    protected bool _useProjectileExplode = true;
    [SerializeField] protected Projectile _explodeProjectilePrefab;
    [SerializeField] protected int _explodeProjectileAmount = 3;
    [Space(10f)] 
    [SerializeField] protected bool _useTrueDamage;
    [SerializeField] protected float _trueDamageRadius = 2.4f;
    [SerializeField] protected LayerMask _damageTarget;
    [SerializeField] protected int _damage = 5;
    
    protected override void DestroyEvent()
    {
        Explode();
    }

    public virtual void Explode()
    {
        
        ExplodeProjectile();
        ExplodeTrueDamage();
    }

    protected virtual void ExplodeProjectile()
    {
        if (_useProjectileExplode)
        {
            Vector2[] directions = VectorCalculator.DirectionsFromCenter(_explodeProjectileAmount);
            for (int i = 0; i < _explodeProjectileAmount; i++)
            {
                Projectile newProjectile = PoolManager.Get(_explodeProjectilePrefab, transform.position, Quaternion.identity);
                newProjectile.Fire(directions[i]);
            }
        }
    }

    protected virtual void ExplodeTrueDamage()
    {
        if (_useTrueDamage)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position, _trueDamageRadius,
                _damageTarget);

            if (hits == null) return;
            
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IDamageable>().TakeDamage(_damage);
            }
        }
    }
}