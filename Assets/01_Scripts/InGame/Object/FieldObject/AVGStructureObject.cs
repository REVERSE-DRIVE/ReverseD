using System;
using UnityEngine;

public class AVGStructureObject : FieldObject
{
    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private SpriteRenderer _thisObjectSpriteRenderer;
    [SerializeField] private ParticleSystem _destroyParticle;
    [SerializeField] private float shieldRadius = 1.5f;
    [Header("State Information")] 
    [SerializeField] private bool isShieldActivated;
    
    private void Awake()
    {
        transform.Find("Visual").GetComponent<SpriteRenderer>();
        
    }

    public override void SetDefault()
    {
        base.SetDefault();
        isShieldActivated = false;
    }

    public void Active()
    {
        _thisObjectSpriteRenderer.enabled = true;
        isShieldActivated = true;
        SetDefault();
    }

    /**
     * <param name="hitDamage">
     * 만약 실드가 없이 직격으로 피격시 들어올 AVG보스의 버스트 대미지
     * </param>
     * <summary>
     * 
     * </summary>
     */
    public void DefenseAVGBurst(int hitDamage)
    {
        if (isShieldActivated) return;
        bool isSafeZone = 
            Vector2.Distance(
                GameManager.Instance._PlayerTransform.position, 
                transform.position) < shieldRadius;
        if (isSafeZone)
        {
            GameManager.Instance._Player.TakeDamage(hitDamage);
        }
        
    }
    
    


    protected override void DestroyEvent()
    {
        _shieldObject.SetActive(true);
        _destroyParticle.Play();
    }

    public override void Destroy()
    {
        DestroyEvent();
        _thisObjectSpriteRenderer.enabled = false;
    }
}