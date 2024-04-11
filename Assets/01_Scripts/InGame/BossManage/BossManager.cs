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
    
    [Space(10)]
    [Header("Boss CutScene")] 
    
    [SerializeField] private TextMeshProUGUI bossNameText;

    
    [Space(10)]
    [SerializeField] private RectTransform _upEdge;
    [SerializeField] private Vector2 _defaultPos1;
    [SerializeField] private Vector2 _targetPos1;
    private ParticleSystem _upParticle;
    [Space(10)]
    [SerializeField] private RectTransform _downEdge;
    [SerializeField] private Vector2 _defaultPos2;
    [SerializeField] private Vector2 _targetPos2;
    private ParticleSystem _downParticle;
    [Space(10)]
    [SerializeField] private RectTransform _bossImageTransform;
    [SerializeField] private Vector2 _defaultPos3;
    [SerializeField] private Vector2 _targetPos3;

    private const float CUTSCENE_ENTER_DURATION = 0.3f;
    private const float CUTSCENE_OUT_DURATION = 0.18f;

    
    [SerializeField] private Image _bossImage;
    
    [SerializeField] private float _cutSceneDisplayDuration = 2;
    
    private SoundObject _soundObject;

    
    
    
    
    private void Awake()
    {
        _soundObject = GetComponent<SoundObject>();
        _upParticle = _upEdge.transform.GetComponentInChildren<ParticleSystem>();
        _downParticle = _downEdge.transform.GetComponentInChildren<ParticleSystem>();
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
        SetBossText();
        ShowCutScene();
        
        
    }
    

    /**
     * <summary>
     * 보스 컷씬 기본 설정 적용
     * </summary>
     * <param name="index">보스 번호</param>
     */
    public void SetBossText()
    {
        bossNameText.text = bossDatas[_currentBossIndex].bossName;
        bossNameText.font = bossDatas[_currentBossIndex].font;
        bossNameText.color = bossDatas[_currentBossIndex].bossNameColor;

        _bossImage.sprite = bossDatas[_currentBossIndex].bossImage;
        _bossImage.SetNativeSize();
        _bossImage.rectTransform.sizeDelta *= bossDatas[_currentBossIndex].spriteSizeOffset;
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
            print("켜짐");
            _upParticle.Play();
            _downParticle.Play();
            _upEdge.DOAnchorPos(_targetPos1, CUTSCENE_ENTER_DURATION);
            _downEdge.DOAnchorPos(_targetPos2, CUTSCENE_ENTER_DURATION);
            _bossImageTransform.DOAnchorPos(_targetPos3, CUTSCENE_ENTER_DURATION);
            
        }
        else
        {
            print("꺼짐");
            _upParticle.Stop();
            _downParticle.Stop();
            _upEdge.DOAnchorPos(_defaultPos1, CUTSCENE_OUT_DURATION);
            _downEdge.DOAnchorPos(_defaultPos2, CUTSCENE_OUT_DURATION);
            _bossImageTransform.DOAnchorPos(_defaultPos3, CUTSCENE_OUT_DURATION);
        }
    }
}