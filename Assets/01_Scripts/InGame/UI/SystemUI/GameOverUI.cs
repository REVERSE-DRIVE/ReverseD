using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private RectTransform _rectTrm;
    [SerializeField] private Image _progressGauge;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
    }
    
    
    
    
    
    
}