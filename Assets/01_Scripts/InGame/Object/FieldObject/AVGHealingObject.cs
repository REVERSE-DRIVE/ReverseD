using UnityEngine;

public class AVGHealingObject : FieldObject
{
    public bool isActive = true;
    [SerializeField]
    private ParticleSystem _generateParticle;

    private void Awake()
    {
        _generateParticle = GetComponentInChildren<ParticleSystem>();
    }
    

    protected override void DestroyEvent()
    {
        EffectObject effectObject = PoolManager.Get(_destroyParticle, transform.position, Quaternion.identity);
        effectObject.Play();
    }

    public void OnCore()
    {
        gameObject.SetActive(true);
        _generateParticle.Play();
        SetDefault();
    }

    private void SetDefault()
    {
        isActive = true;
        hp = hpMax;
    }

    public override void Destroy()
    {
        DestroyEvent();
        isActive = false;
        gameObject.SetActive(false);
    }
}