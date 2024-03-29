using System;
using System.Collections;
using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private float disableTime = 0.5f;

    private Transform _parent;
    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        _parent = FindObjectOfType<Player>().transform.GetChild(1);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            transform.parent = null;
            if (time > disableTime)
            {
                transform.parent = _parent;
                PoolManager.Release(gameObject);
                yield break;
            }
            Fire(transform.rotation * Vector2.right);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PoolManager.Release(gameObject);
        }
    }

    public override void Fire(Vector2 direction)
    {
        base.Fire(direction);
        _rigidbody2D.velocity = direction * (_speed * TimeManager.TimeScale);
    }

}
