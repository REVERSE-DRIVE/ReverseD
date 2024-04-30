using System;
using System.Collections;
using DG.Tweening;
using EnemyManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    
    [Header("Boss Setting")]
    [SerializeField] private BossData[] bossDatas;

    [SerializeField] private int _currentBossIndex;

    
    
    
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    [ContextMenu("DebugBossCutScene")]
    private void DebugStartBoss()
    {
        StartBoss(_currentBossIndex);
    }
    
    public void StartBoss(int bossIndex)
    {
        _currentBossIndex = bossIndex;
        //SetBossText();
        //ShowCutScene();
        
        
    }
    

    
}