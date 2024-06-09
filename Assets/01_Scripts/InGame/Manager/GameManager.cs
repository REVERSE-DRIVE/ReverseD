using EffectManage;
using InGameScene;
using RoomManage;
using SaveDatas;
using UnityEngine;



public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController _PlayerController { get; private set; }
    public Player _Player { get; private set; }
    public Transform _PlayerTransform { get; private set; }
    public UIManager _UIManager { get; private set; }
    public StageManager _StageManager { get; private set; }
    public PlayerEffectManager _PlayerEffectManager { get; private set; }
    public RoomGenerator _RoomGenerator { get; private set; }
    public RenderingManager _RenderingManager { get; private set; }
    public RoomManager _RoomManager { get; private set; }
    public CameraManager _CameraManager { get; private set; }
    public BossManager _BossManager { get; private set; }


    // =====
   
    [SerializeField] private Transform _defaultEnemyParentTrm;
    [SerializeField] private Transform _defaultProjectilesParentTrm;
    public Transform DefaultEnemyParentTrm => _defaultEnemyParentTrm;
    public Transform DefaultProjectilesParentTrm => _defaultProjectilesParentTrm;


    private void Awake()
    {
        _PlayerController = FindObjectOfType<PlayerController>();
        _PlayerTransform = _PlayerController.transform;
        _Player = _PlayerTransform.GetComponent<Player>();
        _PlayerEffectManager = _PlayerTransform.GetComponent<PlayerEffectManager>();
        _UIManager = FindObjectOfType<UIManager>();
        _StageManager = FindObjectOfType<StageManager>();
        _RoomGenerator = FindObjectOfType<RoomGenerator>();
        _RenderingManager = FindObjectOfType<RenderingManager>();
        _RoomManager = FindObjectOfType<RoomManager>();
        _CameraManager = FindObjectOfType<CameraManager>();
    }

    private void Start()
    {
        _RenderingManager._Start();
        Player.OnPlayerHpChangedEvent += _CameraManager.ShakeHit;
        _StageManager.StageStartEvent += _CameraManager.StageStartCameraZoomEvent;
        _Player.OnPlayerDieEvent += HandleGameOver;

        // 게임 시작전에 InGameData를 만들어줘야됨
        InGameData inGameData = DBManager.LoadInGameData();
        _StageManager.Initialize(inGameData.infectLevel, inGameData.detectionCount);
    }


    public void HandleGameOver()
    {
        TimeManager.TimeScale = 0;
        // 게임오버 UI 띄우기
        // -> 시작 창으로 나가는 UI
        // -> 얻은 아이템 정산하는 UI
        // -> 처치한 백신의 수 표시
        
        
    }

    public void Infect(int amount)
    {
        _StageManager.AddInfect(amount);
        
        _UIManager.ShowInfectionAlert(_StageManager.InfectedLevel);
    }

}
