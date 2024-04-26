using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private float _playerDetectRange = 13;
    [SerializeField] private bool _isActiveBossRoom;
    [SerializeField] private LayerMask _playerLayer;

    private Vector2 centerPos;
    private CinemachineVirtualCamera _virtualCamera;
    private void Awake()
    {
        centerPos = transform.position;
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (!_isActiveBossRoom)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(centerPos, _playerDetectRange, _playerLayer);
            if (playerCollider != null)
            {
                _isActiveBossRoom = true;
                BossStart();
            }
        }
    }

    public void BossStart()
    {
        DOTween.To(() => _virtualCamera.m_Lens.OrthographicSize, x => _virtualCamera.m_Lens.OrthographicSize = x, 8,
                1.5f)
            .OnComplete(() =>
            {
                Debug.Log("Boss Start");
            });
    }
}
