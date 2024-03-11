using System;
using UnityEngine;

public class EffectObject: MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        
        
    }
    
    
}