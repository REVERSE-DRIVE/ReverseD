using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    private RectTransform _rectTrm;
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _defaultPosY;

    [SerializeField] private Button _BtnContinue;
    [SerializeField] private Button _BtnQuit;
        
    [SerializeField] private float _UIMoveDuration;
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
        
        _BtnContinue.onClick.AddListener(SetOffUI);
        _BtnQuit.onClick.AddListener(SetOffUI);
    }

    public void SetOffUI()
    {
        _rectTrm.DOAnchorPosY(_defaultPosY, _UIMoveDuration);
        _canvasGroup.DOFade(0, _UIMoveDuration);
        SetCanvasGroup(false);
    }

    public void SetOnUI()
    {
        _rectTrm.DOAnchorPosY(0, _UIMoveDuration);
        _canvasGroup.DOFade(1, _UIMoveDuration);
        SetCanvasGroup(true);
    }

    private void SetCanvasGroup(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }

    private void MoveToMainLobby()
    {
        SceneManager.LoadScene("MainLobbyScene");
    }
}
