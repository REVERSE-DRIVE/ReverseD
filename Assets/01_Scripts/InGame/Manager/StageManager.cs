using System;
using System.Collections;
using RoomManage;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public event Action StageStartEvent; 
    [SerializeField] private Portal _portalPrefab;
    
    [SerializeField] private int _currentChapter = 1;
    [SerializeField] private int _currentStageNum = 0;
    [SerializeField] private int _maxStageIndexInChapter = 5;

    [SerializeField] private float _delayNewStageMsg = 2;
    [SerializeField] private bool _isBossRoomOpened;

    [Header("BossShow Setting")]
    [SerializeField] private Transform _bossRoomZoomTrm;
    [SerializeField] private BossRoom _bossRoomPrefab;
    [SerializeField] private Transform _bossRoomParent;
    private void Start()
    {
        NextStage();
    }


    /**
    * <summary>
    * 디바이스의 감염정도
    * </summary>
    */
    private int infectedLevel = 0;
    public int InfectedLevel => infectedLevel;

    public void AddInfect(int amount)
    {
        infectedLevel += amount;
        infectedLevel = Mathf.Clamp(infectedLevel, 0, 100);
        if (!_isBossRoomOpened)
        {
            OpenBossRoom();
        }
    }
    
    /**
     * <summary>
     * 다음 스테이지로 이동하는 함수
     * </summary>
     */
    public void NextStage()
    {
        _currentStageNum++;
        StartCoroutine(MoveToNextStageCoroutine());
        
        StartCoroutine(ShowNewStageCoroutine());
    }


    private IEnumerator ShowNewStageCoroutine()
    {
        GameManager.Instance._UIManager.ShowStageChangeEvent();
        yield return new WaitForSeconds(_delayNewStageMsg);
        GameManager.Instance._UIManager.ShowNewStageUI(_currentChapter, _currentStageNum);

    }
    
    
    private IEnumerator MoveToNextStageCoroutine()
    {
        
        GameManager.Instance._PlayerTransform.position = Vector3.zero;
        float term = GameManager.Instance._UIManager.LoadingDuration * 0.5f;
        yield return new WaitForSeconds(term);
        
        GameManager.Instance._RoomGenerator.ResetMap();
        if (_currentStageNum < _maxStageIndexInChapter)
        {
            GeneratePortal();
            
        }
        else
        {
            // 챕터의 끝에 도달했으면 보스방을 생성
            GenerateBossRoom();
        }

    }

    private void GeneratePortal()
    {
        Vector2 generatePos = GameManager.Instance._RoomGenerator.LastRoom.transform.position;
        PoolManager.Get(_portalPrefab, generatePos, Quaternion.identity);
    }

    private void GenerateBossRoom()
    {
        Vector2 generatePos = GameManager.Instance._RoomGenerator.FirstRoom.transform.position;
        BossRoom bossRoomPrefab = 
            PoolManager.Get(
                _bossRoomPrefab, generatePos + new Vector2(-1, -37), Quaternion.identity);
    }

    public void OpenBossRoom()
    {
        // RoomGenerator에 있는 FirstRoom 프로퍼티에서 1번 인덱스의 문을 열면 됨
        // 그럼 시작방의 아래쪽 문이 열림

        StartCoroutine(OpenBossRoomCoroutine());
    }

    private IEnumerator OpenBossRoomCoroutine()
    {
        GameManager.Instance._CameraManager.Follow(_bossRoomZoomTrm, 5);
        yield return new WaitForSeconds(2f);
        GameManager.Instance._RoomGenerator.FirstRoom.GetComponent<Room>().OpenDoor(1);
    }
}
