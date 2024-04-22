using UnityEngine;

public class GuidedProjectile : Projectile
{
    [Header("Guided Setting")]
    [Range(0.1f, 10)]
    [SerializeField] private float _accuracy = 0.5f;
    
    private Vector2 _previousDirection;
    private Transform _playerTrm;

    protected override void Awake()
    {
        base.Awake();
        _playerTrm = GameManager.Instance._PlayerTransform;
    }

    protected override void Update()
    {
        if (TimeManager.TimeScale == 0)
            return;

        _previousDirection = _rigid.velocity;
        base.Update();
        GuideDirection();
    }

    private void GuideDirection()
    {
        Vector2 targetDirection = (_playerTrm.position - transform.position).normalized;
        _rigid.velocity = _previousDirection * targetDirection * _accuracy;
    }


    protected override void DestroyEvent()
    {
    }
}