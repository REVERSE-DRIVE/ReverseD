using System;
using System.Collections;
using System.Collections.Generic;
using InGameScene;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController _PlayerController { get; private set; }
    public Transform _PlayerTransform { get; private set; }
    public UIManager _UIManager { get; private set; }

    private void Awake()
    {
        _PlayerController = FindObjectOfType<PlayerController>();
        _PlayerTransform = _PlayerController.transform;
        _UIManager = FindObjectOfType<UIManager>();
        
        
    }
    
    
}
