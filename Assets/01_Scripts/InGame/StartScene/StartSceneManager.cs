using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using UnityEngine;
using EasySave.Json;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [Header("Warning Panel")]
    [SerializeField] private RectTransform _warningPanel;
    [SerializeField] private Ease _ease;
    [SerializeField] private Vector2 _upTarget;
    [SerializeField] private Vector2 _downTarget;

    [Space(20)] 
    [SerializeField] private UnityEvent _onStart;
    [SerializeField] private UnityEvent _realStartGameEvent;
    [SerializeField] private UnityEvent _startGameEvent;
    
    private readonly string _saveDataPath = EasyToJson.LocalPath;
    private PlayerStatus _playerStatus;
    private int count = 0;


    private void Start()
    {
        _onStart.Invoke();
    }

    /**
     * <summary>
     * 게임 시작
     * </summary>
     */
    public void StartGame()
    {
        Debug.Log(_saveDataPath);
        if (File.Exists(_saveDataPath + "PlayerStatus.json"))
        {
            Debug.Log("폴더가 존재합니다.");
            _warningPanel.gameObject.SetActive(true);
            _warningPanel.DOAnchorPos(_upTarget, 0.5f).SetEase(_ease);
        }
        else
        {
            Debug.Log("폴더가 존재하지 않습니다.");
            StartCoroutine(DelaySceneChange());
        }
    }
    
    
    /**
     * <summary>
     * 이어하기
     * </summary>
     */
    public void ContinueGame()
    {
        Debug.Log("게임을 이어서 시작합니다.");
        _playerStatus = EasyToJson.FromJson<PlayerStatus>("PlayerStatus");
    }

    /**
     * <summary>
     * 파일 삭제 확인
     * </summary>
     */
    public void CheckFileOverride()
    {
        ++count;
        _startGameEvent.Invoke();
        if (count != 2) return;
        
        StartCoroutine(DelaySceneChange());
        count = 0;
    }
    private IEnumerator DelaySceneChange()
    {
        _realStartGameEvent.Invoke();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainLobyScene");
    }
    
    /**
     * <summary>
     * 취소
     * </summary>
     */
    public void Deny()
    {
        _warningPanel.DOAnchorPos(_downTarget, 0.5f).SetEase(_ease)
            .OnComplete(() => _warningPanel.gameObject.SetActive(false));
    }
}
