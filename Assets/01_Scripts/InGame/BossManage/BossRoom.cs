using System.Collections;
using Cinemachine;
using DG.Tweening;
using RoomManage;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private float _playerDetectRange = 13;
    [SerializeField] private bool _isActiveBossRoom;
    [SerializeField] private LayerMask _playerLayer;

    private Vector2 centerPos;
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _parent;
    private bool _isCutScene;
    private void Awake()
    {
        centerPos = transform.position;
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _parent = FindObjectOfType<Grid>().transform;
    }

    private void Update()
    {
        bool isPlayerInLastRoom = Vector3.Distance(
            GameManager.Instance._PlayerTransform.position,
            GameManager.Instance._RoomGenerator.LastRoom.transform.position) < 5f;
        if (isPlayerInLastRoom && !_isCutScene)
        {
            StartCoroutine(CutScene());
        }
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
    
    private IEnumerator CutScene()
    {
        gameObject.SetActive(true);
        Debug.Log("CutScene Start");
        transform.SetParent(_parent);
        _virtualCamera.Follow = transform.GetChild(2);
        TimeManager.TimeScale = 0f;
        yield return new WaitForSeconds(2);
        Debug.Log("CutScene End");
        TimeManager.TimeScale = 1f;
        _isCutScene = true;
        _virtualCamera.Follow = GameManager.Instance._PlayerTransform;
    }
}
