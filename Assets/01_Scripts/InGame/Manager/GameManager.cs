using System;
using System.Collections;
using System.Collections.Generic;
using InGameScene;
using RoomManage;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController _PlayerController { get; private set; }
    public Transform _PlayerTransform { get; private set; }
    public UIManager _UIManager { get; private set; }

    public RoomGenerator _RoomGenerator { get; private set; }
    public RenderingManager _RenderingManager { get; private set; }


    private void Awake()
    {
        _PlayerController = FindObjectOfType<PlayerController>();
        _PlayerTransform = _PlayerController.transform;
        _UIManager = FindObjectOfType<UIManager>();
        _RoomGenerator = FindObjectOfType<RoomGenerator>();
        _RenderingManager = FindObjectOfType<RenderingManager>();
        
        
    }

    private void Start()
    {
        _RenderingManager._Start();
    }
}
