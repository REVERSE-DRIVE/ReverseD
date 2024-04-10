using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    
    [Header("Boss Setting")]
    [SerializeField] private BossData[] bossDatas;

    [SerializeField] private int _currentBossIndex;
    
    [Space(10)]
    [Header("Boss CutScene")] 
    
    [SerializeField] private TextMeshProUGUI bossNameText;

    
    [Space(10)]
    [SerializeField] private RectTransform _upEdge;
    [SerializeField] private Vector2 _defaultPos1;
    [SerializeField] private Vector2 _targetPos1;
    [Space(10)]
    [SerializeField] private RectTransform _downEdge;
    [SerializeField] private Vector2 _defaultPos2;
    [SerializeField] private Vector2 _targetPos2;
    [Space(10)]
    [SerializeField] private RectTransform _bossImageTransform;
    [SerializeField] private Vector2 _defaultPos3;
    [SerializeField] private Vector2 _targetPos3;
    
    [SerializeField] private Image _bossImage;
    
    [SerializeField] private float _cutSceneDisplayDuration = 2;
    
    
    private SoundObject _soundObject;

    
    
    
    
    private void Awake()
    {
        _soundObject = GetComponent<SoundObject>();
    }

    private void Start()
    {
        StartBoss(_currentBossIndex);
    }

    public void StartBoss(int bossIndex)
    {
        _currentBossIndex = bossIndex;
        SetBossText(_currentBossIndex);
        ShowCutScene();
        
        
    }
    

    /**
     * <summary>
     * 보스 컷씬 기본 설정 적용
     * </summary>
     * <param name="index">보스 번호</param>
     */
    public void SetBossText(int index)
    {
        bossNameText.text = bossDatas[index].bossName;
        bossNameText.font = bossDatas[index].font;
        bossNameText.color = bossDatas[index].bossNameColor;

        _bossImage.SetNativeSize();
        _bossImage.rectTransform.sizeDelta *= 7f;
    }

    public void ShowCutScene()
    {
        _soundObject.PlayAudio(1);
        StartCoroutine(ShowCutSceneCoroutine());

    }
    private IEnumerator ShowCutSceneCoroutine()
    {
        Appear(true);
        yield return new WaitForSeconds(_cutSceneDisplayDuration);
        Appear(false);
    }

    private void Appear(bool isOnOff)
    {
        if (isOnOff)
        {
            _upEdge.DOAnchorPos(_targetPos1, 0.15f);
            _downEdge.DOAnchorPos(_targetPos2, 0.15f);
            _bossImageTransform.DOAnchorPos(_targetPos3, 0.15f);
        }
        else
        {
            
            _upEdge.DOAnchorPos(_defaultPos1, 0.15f);
            _downEdge.DOAnchorPos(_defaultPos2, 0.15f);
            _bossImageTransform.DOAnchorPos(_defaultPos3, 0.15f);
        }
    }
}