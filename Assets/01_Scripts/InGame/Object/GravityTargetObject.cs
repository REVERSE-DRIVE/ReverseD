using System;
using Unity.VisualScripting;
using UnityEngine;

public class GravityTargetObject : MonoBehaviour
{
    [SerializeField] private bool isAffectGravity = true;
    
    [SerializeField]
    private Vector2 _gravityDirection;
    [SerializeField]
    private float _gravityScale = 1;
    private bool isOnGravity;

    private Rigidbody2D _rigid;

        
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        GravityManager.GravityChangedEvent += RefreshGravityInfo;

    }

    private void RefreshGravityInfo(bool isOnGravity, Vector2 direction, float gravityScale = 1)
    {
        this.isOnGravity = isOnGravity;
        _gravityDirection = direction;
        _gravityScale = gravityScale;
    }


    private void Update()
    {
        if (!isOnGravity || !isAffectGravity) return;
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (TimeManager.TimeScale == 0) return;
        Vector2 dir = _gravityDirection * _gravityScale;
        _rigid.velocity += dir;
    }
}