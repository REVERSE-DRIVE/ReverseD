using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LogoEffect : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private Image[] logoImages;
    [SerializeField] private Ease logoMoveEase;
    [SerializeField] private float logoMoveDuration;
    [SerializeField] private float waitTime;
    private void Start()
    {
        StartCoroutine(LogoEffectCoroutine());
    }
    
    private IEnumerator LogoEffectCoroutine()
    {
        logoImages[0].transform.DOMoveX(0, logoMoveDuration).SetEase(logoMoveEase);
        yield return new WaitForSeconds(waitTime);
        logoImages[1].transform.DOMoveX(0, logoMoveDuration).SetEase(logoMoveEase);
    }
}
