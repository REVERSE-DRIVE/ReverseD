using System;
using EntityManage;
using UnityEngine;

public class ClearTileZone : MonoBehaviour
{
    private float _damageCoolTime = 1f;
    private string _playerTagString = "Player";
    private float _currentTime = 0;

    [SerializeField]
    private bool _enterance;
    private Transform _targetTrm;
    [SerializeField]
    private bool _isFirstDamage = true;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_playerTagString))
        {
            _enterance = true;
            _targetTrm = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(_playerTagString))
        {
            _enterance = false;
            _targetTrm =null;
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_enterance)
        {
            DamageCast(_targetTrm);            

        }
    }

    private void DamageCast(Transform targetTrm)
    {
        
        if (targetTrm.TryGetComponent(out IDamageable hit))
        {
            if (_currentTime >= _damageCoolTime)
            {
                _currentTime = 0;
                if (_isFirstDamage)
                {
                    _isFirstDamage = false;
                    ComputerManager.Instance.Detect();
                }
                
                hit.TakeDamage(5);
                
            }
            
        }
    }
}
