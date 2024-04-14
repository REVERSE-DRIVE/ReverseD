using UnityEngine;

public class AVGStructureObject : FieldObject
{
    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private SpriteRenderer _thisObjectVisual;
    [SerializeField] private ParticleSystem _reuseDestroyParticle;
    [SerializeField] private Collider2D _thisObjectCollider;
    private Transform _bossTrm;
    [SerializeField] private float shieldRadius = 1.5f;
    [Header("State Information")] 
    [SerializeField] private bool isShieldActivated;
    
    private void Awake()
    {
        transform.Find("Visual").GetComponent<SpriteRenderer>();
        _thisObjectCollider = GetComponent<Collider2D>();
    }

   

    public void Active(Transform bossTrm)
    { // 레드 스테이트가 활성화됨
        _bossTrm = bossTrm;
        transform.position =  (Vector2)_bossTrm.position + (Random.insideUnitCircle * 9);
        SetDefault();
    }

    public override void SetDefault()
    {
        base.SetDefault();
        _shieldObject.SetActive(false);
        isShieldActivated = false;
        _thisObjectVisual.enabled = true;
        _thisObjectCollider.enabled = true;
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
        bool isSafeZone = 
            Vector2.Distance(
                GameManager.Instance._PlayerTransform.position, 
                transform.position) < shieldRadius;
        if (isSafeZone && isShieldActivated) return;
        GameManager.Instance._Player.TakeDamage(hitDamage);
        
    }
    
    


    protected override void DestroyEvent()
    {
        _shieldObject.SetActive(true);
        _reuseDestroyParticle.Play();
    }

    public override void Destroy()
    {
        DestroyEvent();
        _thisObjectVisual.enabled = false;
        isShieldActivated = true;
    }

    public void OffObject()
    {
        _shieldObject.SetActive(false);
        _thisObjectVisual.enabled = false;
        _thisObjectCollider.enabled = false;
    }
}