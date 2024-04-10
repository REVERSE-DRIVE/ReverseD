using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banner : MonoBehaviour
{
    [SerializeField] private bool isUp;
    [SerializeField] private Ease ease;
    private RectTransform rectTransform;
    [SerializeField]
    private Vector2 _defaultPos;
    [SerializeField] private RectTransform targetPoint;

    [SerializeField] private bool _onOff;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _defaultPos = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        AppearUI();
    }
    

    private void AppearUI()
    {
        if (_onOff) return;
        rectTransform.DOAnchorPos(targetPoint.anchoredPosition, 1f).SetEase(ease);

    }

    private void DisappearUI()
    {
        if (!_onOff) return;
        rectTransform.DOLocalMove(_defaultPos, 1f).SetEase(ease);

    }
    
}
