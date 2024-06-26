﻿using System.Collections;
using EnemyManage;
using UnityEngine;

public class PushObject : InteractionObject
{
    [SerializeField] private bool isActive;

    private Rigidbody2D _rigid;
    [SerializeField] private float _pushPower = 10;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _damageVelocity = 1.5f;
    
    [SerializeField] private bool canDamage;
    private Vector2 previousDirection;

    private FieldObject _fieldObject;
    private SoundObject _soundObject;

    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody2D>();
        _soundObject = GetComponent<SoundObject>();
        _fieldObject = GetComponent<FieldObject>();
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
        else
        {
            _rigid.velocity = -_rigid.velocity * 1.5f;
        }
        
        
    }

    private IEnumerator PushRoutine(Vector2 direction)
    {
        _rigid.bodyType = RigidbodyType2D.Dynamic;
        SetObjectActive(true);
        _rigid.AddForce(direction * _pushPower, ForceMode2D.Impulse);
        while (_rigid.velocity.sqrMagnitude > 0.5f)
        {
            previousDirection = _rigid.velocity;
            yield return new WaitForSeconds(0.05f);
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
        _soundObject.PlayAudio(1);

        if (isActive && _rigid.velocity.sqrMagnitude > 2)
        {
            //other.transform.GetComponent<Enemy>().TakeStrongDamage(_damage);
            if (other.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeStrongDamage(_damage);
                _fieldObject.TakeDamage(1);
            }else if (other.transform.TryGetComponent(out Player player))
            {
                player.TakeStrongDamage(_damage);
                _fieldObject.TakeDamage(1);
            }
            
        }

        if (other.transform.CompareTag("Wall"))
        {
            Vector2 dir = RayManager.Reflect(transform.position, previousDirection.normalized, 10,
                LayerMask.GetMask("Wall"));

            _rigid.bodyType = RigidbodyType2D.Dynamic;
            _fieldObject.TakeDamage(1);
            _rigid.AddForce(dir.normalized * (previousDirection * 0.6f), ForceMode2D.Impulse);
        }
        
        SetObjectActive(true);

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