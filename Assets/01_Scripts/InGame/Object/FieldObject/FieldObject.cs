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
            Destroy();
        }
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
