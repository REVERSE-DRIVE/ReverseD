using System.IO;
using DG.Tweening;
using UnityEngine;
using EasySave.Json;
using UIManage;
using UnityEngine.Events;

public class StartSceneManager : MonoBehaviour
{
    [Header("Warning Panel")]
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private Ease _ease;
    [SerializeField] private RectTransform _target;
    [Space(20)]
    [SerializeField] private UnityEvent _startGameEvent;
    private readonly string _saveDataPath = EasyToJson.LocalPath;
    private PlayerStatus _playerStatus;
    private int count = 0;
    public void StartGame()
    {
        Debug.Log(_saveDataPath);
        if (File.Exists(_saveDataPath + "PlayerStatus.json"))
        {
            Debug.Log("폴더가 존재합니다.");
            _warningPanel.SetActive(true);
            _warningPanel.transform.DOLocalMove(_target.localPosition, 0.5f).SetEase(_ease);
        }
        else
        {
            Debug.Log("폴더가 존재하지 않습니다.");
        
        }
    }
    
    public void ContinueGame()
    {
        Debug.Log("게임을 이어서 시작합니다.");
        _playerStatus = EasyToJson.FromJson<PlayerStatus>("PlayerStatus");
    }

    public void CheckFileDelete()
    {
        ++count;
        if (count == 2)
        {
            _startGameEvent.Invoke();
        }
    }
}
