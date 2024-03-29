using System;
using System.Collections;
using UnityEngine;

public class EffectObject: MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private bool OnPlayEnable = true;
    private void Awake()
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        
        
    }

    private void OnEnable()
    {
        if (OnPlayEnable)
        {
            Play();
        }
    }

    public void Play()
    {
        _particleSystem.Play();
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        PoolManager.Release(gameObject);
    } 
}