using System;
using System.Collections;
using UnityEngine;

public class PushObject : InteractionObject
{
    [SerializeField] private bool isActive;

    private Rigidbody2D _rigid;
    [SerializeField] private float _pushPower = 10;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _damageVelocity = 1.5f;
    [SerializeField]
    private float velocity = 0;

    [SerializeField] private bool canDamage;


    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isActive)
        {
            CheckStop();   
        }
    }

    public override void Interact()
    {
        base.Interact();
        
        if (!isActive)
        {
            
            
            Vector2 direction = transform.position - GameManager.Instance._PlayerTransform.position;
            StartCoroutine(PushRoutine(direction));

        }
        
        
    }

    private IEnumerator PushRoutine(Vector2 direction)
    {
        _rigid.bodyType = RigidbodyType2D.Dynamic;
        SetObjectActive(true);
        _rigid.AddForce(direction * _pushPower, ForceMode2D.Impulse);
        while (_rigid.velocity.sqrMagnitude > 0.5f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        SetObjectActive(false);

    }

    private void SetObjectActive(bool value)
    {
        isActive = value;
        canDamage = value;
        canInteraction = !value;
    }
    
        

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isActive && canDamage)
        {
            Entity entity = other.transform.GetComponent<Entity>();
            if (entity)
            {
                entity.TakeDamage(_damage);
            }
            
        }

        if (CompareTag("Wall"))
        {
            print("벽과 충돌함");
            Vector2 dir = RayManager.Reflect(transform.position, _rigid.velocity.normalized, 10, LayerMask.GetMask("Wall"));

            Vector2 velocity = _rigid.velocity;
            _rigid.velocity = Vector2.zero;
            _rigid.AddForce(dir * (velocity.sqrMagnitude * 0.6f), ForceMode2D.Impulse);
        }
    }

    private void CheckStop()
    {
        if (_rigid.velocity.sqrMagnitude < 0.05f)
        {
            Stop();
        }
    }

    private void Stop()
    {
        _rigid.velocity = Vector2.zero;
        _rigid.bodyType = RigidbodyType2D.Static;
        SetObjectActive(false);
    }
    
}