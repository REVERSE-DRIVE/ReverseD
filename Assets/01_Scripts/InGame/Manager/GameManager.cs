using System;
using System.Collections;
using System.Collections.Generic;
using InGameScene;
using RoomManage;
using Unity.Collections;
using UnityEngine;



public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController _PlayerController { get; private set; }
    public Player _Player { get; private set; }
    public Transform _PlayerTransform { get; private set; }
    public UIManager _UIManager { get; private set; }

    public RoomGenerator _RoomGenerator { get; private set; }
    public RenderingManager _RenderingManager { get; private set; }
    public RoomManager _RoomManager { get; private set; }

    public CameraManager _CameraManager { get; private set; }


    // =====
    /**
     * <summary>
     * 디바이스의 감염정도
     * </summary>
     */
    private int infectedLevel = 0;
    public int InfectedLevel => infectedLevel;

    [SerializeField] private Transform _defaultEnemyParentTrm;

    public Transform DefaultEnemyParentTrm
    {
        get { return _defaultEnemyParentTrm; }
        private set { }
    }
    
    
    
    
    
    private void Awake()
    {
        _PlayerController = FindObjectOfType<PlayerController>();
        _Player = FindObjectOfType<Player>();
        _PlayerTransform = _PlayerController.transform;
        _UIManager = FindObjectOfType<UIManager>();
        _RoomGenerator = FindObjectOfType<RoomGenerator>();
        _RenderingManager = FindObjectOfType<RenderingManager>();
        _RoomManager = FindObjectOfType<RoomManager>();
        _CameraManager = FindObjectOfType<CameraManager>();
    }

    private void Start()
    {
        _RenderingManager._Start();
        Player.OnPlayerHpChanged += _CameraManager.ShakeHit;
    }


    public void GameOver()
    {
        TimeManager.TimeScale = 0;
    }
}
