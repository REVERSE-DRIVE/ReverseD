using System;
using System.Collections;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    [Header("Setting Values")] [SerializeField]
    private float _size = 1;

    [SerializeField] private float _generateTerm = 1f;

    [SerializeField] private GameObject _generateTarget;
    [SerializeField] private Vector2 _generateOffset;
    
    private ParticleSystem _generateParticle;


    private void OnEnable()
    {
        Generate();
    }

    private void Generate()
    {
        StartCoroutine(GenerateCoroutine());
    }

    private IEnumerator GenerateCoroutine()
    {
        _generateParticle.Play();
        yield return new WaitForSeconds(_generateTerm);
        PoolManager.Get(_generateTarget);

    }
}
