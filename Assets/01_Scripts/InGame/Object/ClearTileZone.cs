using EntityManage;
using UnityEngine;

public class ClearTileZone : MonoBehaviour
{
    private float _damageCoolTime = 1f;
    private string _playerTagString = "Player";
    private float _currentTime = 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_playerTagString))
        {
            DamageCast(other.transform);            
        }
    }

    private void DamageCast(Transform targetTrm)
    {
        if (targetTrm.TryGetComponent(out IDamageable hit))
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _damageCoolTime)
            {
                _currentTime = 0;
                hit.TakeDamage(5);

            }
            
        }
    }
}
