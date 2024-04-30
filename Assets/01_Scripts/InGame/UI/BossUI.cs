using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemyManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    
    [Space(10)]
    [Header("Boss CutScene")] 
    [SerializeField]
    private BossBar _bossBar;
    
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

    public void HandleBossUIEvent(BossData bossData)
    {
        ShowCutScene();
        SetBossText(bossData);
    }
    
    /**
     * <summary>
     * 보스 컷씬 기본 설정 적용
     * </summary>
     * <param name="index">보스 번호</param>
     */
    public void SetBossText(BossData bossData)
    {
        bossNameText.text = bossData.bossName;
        bossNameText.font = bossData.font;
        bossNameText.color = bossData.bossNameColor;

        _bossImage.sprite = bossData.bossImage;
        _bossImage.SetNativeSize();
        _bossImage.rectTransform.sizeDelta *= bossData.spriteSizeOffset;
    }

    [ContextMenu("Debug_ShowCutScene")]
    public void ShowCutScene()
    {
        _soundObject.PlayAudio(1);
        StartCoroutine(ShowCutSceneCoroutine());

    }
    private IEnumerator ShowCutSceneCoroutine()
    {
        OnBossUI();
        yield return new WaitForSeconds(_cutSceneDisplayDuration);
        OffBossUI();
    }

    public void OnBossUI()
    {
        
        _upParticle.Play();
        _downParticle.Play();
        _upEdge.DOAnchorPos(_targetPos1, 0.5f);
        _downEdge.DOAnchorPos(_targetPos2, 0.5f);
        _bossImageTransform.DOAnchorPos(_targetPos3, 0.8f);
        
            
    }

    private void OffBossUI()
    {
        
        _upParticle.Stop();
        _downParticle.Stop();
        _upEdge.DOAnchorPos(_defaultPos1, CUTSCENE_OUT_DURATION);
        _downEdge.DOAnchorPos(_defaultPos2, CUTSCENE_OUT_DURATION);
        _bossImageTransform.DOAnchorPos(_defaultPos3, CUTSCENE_OUT_DURATION);

    }
}
