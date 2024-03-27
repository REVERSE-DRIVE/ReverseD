using System;
using System.Collections;
using UnityEngine;

public class PushObject : InteractionObject
{
    [SerializeField] private bool isActive;

    private Rigidbody2D _rigid;
    [SerializeField] private float _pushPower = 10;
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _damageVelocity = 1.5f;

    [SerializeField]
    private float velocity = 0;

    [SerializeField] private bool canDamage;
    public override void Interact()
    {
        
        if (!isActive)
        {
            Vector2 direction = transform.position - GameManager.Instance._PlayerTransform.position;
            StartCoroutine(PushRoutine(direction));

        }
        
        
    }

    private IEnumerator PushRoutine(Vector2 direction)
    {
        canDamage = true;
        _rigid.AddForce(direction * _pushPower, ForceMode2D.Impulse);
        while (_rigid.velocity.sqrMagnitude > 0.5f)
        {
            yield return null;
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isActive && canDamage)
        {
            // 충돌체에 대미지 주기
            
        }
    }
}