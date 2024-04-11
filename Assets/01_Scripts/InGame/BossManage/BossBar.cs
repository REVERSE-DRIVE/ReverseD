using System.Collections;
using DG.Tweening;
using EnemyManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private RectTransform _gaugeTrm;
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

    [SerializeField] private BossData _currentBossInformation;

    private RectTransform _barTransform;
    private TextMeshProUGUI _bossNameText;
    

    
    

    private void Awake()
    {
        _barTransform = transform.GetComponent<RectTransform>();
        _gauge.fillAmount = 1;
    }

    
    public void ShowFirstBar(BossData bossInfo)
    {
        _currentBossInformation = bossInfo;
        SetBossBar(_currentBossInformation.bossNameColor);
        _bossNameText.font = bossInfo.font;
        _bossNameText.text = bossInfo.bossName;
        _bossNameText.color = bossInfo.bossNameColor;
        StartCoroutine(ShowBarRoutine());
    }
    
    private IEnumerator ShowBarRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        
        RefreshGauge(0f,1f);
        
    }

    public void DestroyBossBar()
    {
        
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
        Sequence seq = DOTween.Sequence();
        _barTransform.DOAnchorPosY(_defaultYPos, _showDuration);

        ShakeBar(2f, 0.2f);
    }

    private void ShakeBar(float shakePower, float duration)
    {
        StartCoroutine(ShakeRoutine(shakePower, duration));
    }

    private IEnumerator ShakeRoutine(float shakePower, float duration)
    {
        float currentTime = 0;
        Vector2 defaultTrmPos = _gaugeTrm.anchoredPosition;
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            _gaugeTrm.rotation = Quaternion.Euler(0, 0, Random.Range(-shakePower, shakePower) * 1.5f);
            _gaugeTrm.anchoredPosition = defaultTrmPos + new Vector2(
                Random.Range(-shakePower, shakePower),
                Random.Range(-shakePower, shakePower)).normalized * 3;
            yield return null;

        }

        _gaugeTrm.anchoredPosition = defaultTrmPos;
        _gaugeTrm.rotation = Quaternion.identity;
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
            ShakeBar(2f, 0.3f);
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
