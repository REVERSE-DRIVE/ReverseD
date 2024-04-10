using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Image _gauge;
    [SerializeField] private Image _backGauge;
    [SerializeField] private float termToChange = 0.2f;
    
    [Header("BossBar Setting")]
    [SerializeField] private Color _bossBarColor = Color.cyan;
    [SerializeField] private bool _isGaugeStack;
    [SerializeField] private int _defaultYPos = 100;
    [SerializeField] private int _targetYPos = -40;
    [SerializeField] private float _showDuration = 0.2f;

    [Header("State Information")] [SerializeField]
    private bool _onOff;

    [SerializeField] private BossCutsceneSet _currentBossInformation;

    private RectTransform _barTransform;

    
    

    private void Awake()
    {
        _barTransform = transform.GetComponent<RectTransform>();
        _gauge.fillAmount = 1;
    }
    
    public void ShowFirstBar(BossCutsceneSet bossInfo)
    {
        _currentBossInformation = bossInfo;
        SetBossBar(_currentBossInformation.bossNameColor);
        
    }


    #region BossBar Show Effect

    
    
    public void SetBossBar(Color gaugeColor)
    {
        _bossBarColor = gaugeColor;
        _gauge.color = _bossBarColor;
        _backGauge.color = _bossBarColor * new Color(0.6f,0.6f,0.6f);
    }

    public void MoveOnBar()
    {
        _barTransform.DOAnchorPosY(_targetYPos, _showDuration);
        
    }

    public void MoveOffBar()
    {
        _barTransform.DOAnchorPosY(_defaultYPos, _showDuration);

    }

    #endregion

    #region Gauge Value Change

    
    [ContextMenu("DebugDecrease")]
    private void TestGaugeDecrease()
    {
        RefreshGauge(0.8f, 0.2f);
    }
    [ContextMenu("DebugIncrease")]
    private void TestGaugeIncrease()
    {
        RefreshGauge(0.2f, 0.8f);
    }
    
    /**
     * <param name="beforeValue">
     * 변화 이전의 값
     * </param>
     * <param name="afterValue">
     * 변화 후의 값
     * </param>
     * <param name="max">
     * 게이지 값의 최댓값
     * </param>
     * <summary>
     * 보스바 게이지를 값을 통해 최신화함
     * Value값들은 Max값을 초과할 수 없고 0 보다 작을 수 없음
     * (알아서 막아줌)
     * </summary>
     */
    public void RefreshGauge(int beforeValue, int afterValue, int max)
    {
        float beforePercent = beforeValue / (float)max;
        float afterPercent = afterValue / (float)max;
        RefreshGauge(beforePercent, afterPercent);
    }
    /**
     * <param name="beforePercent">
     * 변화 이전의 백분율 값
     * </param>
     * <param name="afterPercent">
     * 변화 후의 백분율 값
     * </param>
     * <summary>
     * 보스바 게이지를 값을 통해 최신화함
     * Percent값들은 0과 1사이의 float값임 (알아서 막아줌)
     * </summary>
     */
    public void RefreshGauge(float beforePercent, float afterPercent)
    {
        beforePercent = Mathf.Clamp01(beforePercent);
        afterPercent = Mathf.Clamp01(afterPercent);
        StartCoroutine(GaugeFillRoutine(beforePercent, afterPercent));
    } 
    private IEnumerator GaugeFillRoutine(float beforePercent, float afterPercent)
    { 
        if(beforePercent == afterPercent) yield break;
        
        if (beforePercent < afterPercent)
        {   // 변화값이 더 크면 
            _backGauge.fillAmount = afterPercent;
            yield return new WaitForSeconds(termToChange);
            float currentTime = 0;
            while (currentTime < termToChange)
            {
                currentTime += Time.deltaTime;
            
                float time = currentTime / termToChange;
                _gauge.fillAmount = Mathf.Lerp(beforePercent, afterPercent, time);
            
                yield return null;
            }

            _gauge.fillAmount = afterPercent;
        }
        else if (beforePercent > afterPercent)
        {
            _gauge.fillAmount = afterPercent;
            yield return new WaitForSeconds(termToChange);
            float currentTime = 0;
            while (currentTime < termToChange)
            {
                currentTime += Time.deltaTime;
            
                float time = currentTime / termToChange;
                _backGauge.fillAmount = Mathf.Lerp(beforePercent, afterPercent, time);
            
                yield return null;
            }

            _backGauge.fillAmount = afterPercent;
        }
    }


    #endregion
    
}
