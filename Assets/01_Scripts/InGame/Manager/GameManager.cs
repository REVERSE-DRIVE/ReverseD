using EffectManage;
using InGameScene;
using RoomManage;
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

    public Transform DefaultEnemyParentTrm
    {
        get { return _defaultEnemyParentTrm; }
        private set { }
    }
    
    
    
    
    
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
        Player.OnPlayerHpChanged += _CameraManager.ShakeHit;
        _StageManager.StageStartEvent += _CameraManager.StageStartCameraZoomEvent;
    }


    public void GameOver()
    {
        TimeManager.TimeScale = 0;
        
    }

    public void Infect(int amount)
    {
        _StageManager.AddInfect(amount);
        
        _UIManager.ShowInfectionAlert(_StageManager.InfectedLevel);
    }

}
