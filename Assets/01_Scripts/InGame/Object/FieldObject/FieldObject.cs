using System;
using System.Collections;
using UnityEngine;

public abstract class FieldObject : MonoBehaviour
{
    public bool canDestroy = true;
    
    [Header("Status Setting")]
    [SerializeField]
    protected int hp = 3;
    [SerializeField]
    protected int hpMax = 3;

    public int Hp => hp;
    public int HpMax => hpMax;
    public bool IsDestroy => hp <= 0;

    [SerializeField] protected EffectObject _destroyParticle;
    [SerializeField] protected Collider2D _collider;


    protected virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();

    }

    public virtual void SetDefault()
    {
        hp = hpMax;
        _collider.enabled = true;
    }

    public virtual void TakeDamage(int amount)
    {
        if (!canDestroy) return;
        
        hp -= amount;
        CheckIsDestroy();
    }

    protected virtual void CheckIsDestroy()
    {
        if (IsDestroy)
        {
            StartCoroutine(DestroyCoroutine());
        }
    }

    protected IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        _collider.enabled = false;
        Destroy();

    }

    public virtual void Destroy()
    {
        DestroyEvent();
        EffectObject effectObject = PoolManager.Get(_destroyParticle, transform.position, Quaternion.identity);
        effectObject.Play();
        PoolManager.Release(gameObject);
    }

    protected abstract void DestroyEvent();



}
