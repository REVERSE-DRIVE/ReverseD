using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComputerManager : MonoSingleton<ComputerManager>
{
    [SerializeField] private bool _isEmergency;
    public int detectLimitAmount = 1;
    public int detectCount = 0;
    
    [Header("Settings")] 
    [SerializeField] private GameObject _detectedAlertPrefab;

    [SerializeField] private RectTransform _detectCountPanel;
    [SerializeField] private TextMeshProUGUI _detectCountText;
    
    
    private List<GameObject> _alerts = new List<GameObject>();

    [ContextMenu("Detect")]
    public void Detect()
    {
        detectCount++;
        GameManager.Instance._CameraManager.Shake(2, 1);
        StartCoroutine(DetectCoroutine());
        if (detectCount >= detectLimitAmount)
        {
            
        }
    }

    [ContextMenu("DebugDetectComplete")]
    public void DetectComplete()
    {
        StartCoroutine(DetectMarkSpawn());
    }

    private IEnumerator DetectCoroutine()
    {
        GameManager.Instance._RenderingManager.SetVignetteColor(Color.green);
        
        _detectCountPanel.DOAnchorPosY(-130, 0.2f);
        WaitForSeconds ws = new WaitForSeconds(0.15f);
        int randAmount = Random.Range(3, 5);
        for (int i = 0; i < randAmount; i++)
        {
            yield return ws;
            GameObject alert = Instantiate(_detectedAlertPrefab);
            _alerts.Add(alert);
            alert.transform.position = (Vector2)PlayerManager.Instance._playerTrm.position + 
                                       (Random.insideUnitCircle * Random.Range(2.5f, 8f));
        }

        yield return ws;
        _detectCountText.text = (detectLimitAmount - detectCount).ToString();
        yield return new WaitForSeconds(1f);
        _detectCountPanel.DOAnchorPosY(400f, 0.2f);
        GameManager.Instance._RenderingManager.SetVignetteDefault();
    }

    private IEnumerator DetectMarkSpawn()
    {
        WaitForSeconds ws = new WaitForSeconds(0.2f);

        while (true)
        {
            
            yield return ws;
            GameObject alert = Instantiate(_detectedAlertPrefab);
            _alerts.Add(alert);
            alert.transform.position = (Vector2)PlayerManager.Instance._playerTrm.position + 
                                       (Random.insideUnitCircle * Random.Range(2.5f, 8f));
        }
    }
}
