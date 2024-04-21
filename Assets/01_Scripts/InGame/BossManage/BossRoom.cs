using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private float _playerDetectRange = 13;
    [SerializeField] private bool _isActiveBossRoom;
    [SerializeField] private LayerMask _playerLayer;

    private Vector2 centerPos;
    private void Awake()
    {
        centerPos = transform.position;
    }

    private void Update()
    {
        if (_isActiveBossRoom)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(centerPos, _playerDetectRange, _playerLayer);
            if (playerCollider == null)
            {
                _isActiveBossRoom = true;
                BossStart();
            }
        }
    }

    public void BossStart()
    {
        
        //GameManager.Instance._BossManager.StartBoss();
    }
}
