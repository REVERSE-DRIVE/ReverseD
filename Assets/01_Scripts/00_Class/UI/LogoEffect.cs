using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class LogoEffect : MonoBehaviour
{
    [SerializeField] private Renderer2DData renderer2DData;
    [SerializeField] private Material screenMaterial;
    [SerializeField] private Image[] logoImages;
    [SerializeField] private Ease logoMoveEase;
    [SerializeField] private float logoMoveDuration;
    [SerializeField] private float waitTime;
    
    private SoundObject _soundObject;

    private void Awake()
    {
        _soundObject = GetComponent<SoundObject>();
    }

    private void Start()
    {
        StartCoroutine(LogoEffectCoroutine());
    }
    
    private IEnumerator LogoEffectCoroutine()
    {
        _soundObject.PlayAudio(0);
        renderer2DData.rendererFeatures[0].SetActive(true);
        MaterialSet(35, 43, 1);
        
        // 이미지 이동
        logoImages[0].transform.DOMoveX(0, logoMoveDuration).SetEase(logoMoveEase);
        yield return new WaitForSeconds(waitTime);
        logoImages[1].transform.DOMoveX(0, logoMoveDuration).SetEase(logoMoveEase);
        yield return new WaitForSeconds(1.3f);

        MaterialSet(100, 100, 1);
        yield return new WaitForSeconds(waitTime * 4f);
        MaterialSet(10, 10, 1);
        yield return new WaitForSeconds(waitTime * 4f);
        // 이미지 끄기
        renderer2DData.rendererFeatures[0].SetActive(false);
        logoImages[0].enabled = false;
        logoImages[1].enabled = false;
        MaterialSet(35, 43, 1);
        
        yield return new WaitForSeconds(0.15f);
        // 씬 전환
        SceneManager.LoadScene("StartScene");
    }
    
    /**
     * <summary>
     * Material 설정
     * </summary>
     */
    private void MaterialSet(float noiseAmount, float glitchStrenght, float scanLineStrenght)
    {
        screenMaterial.SetFloat("_NoiseAmount", noiseAmount);
        screenMaterial.SetFloat("_GlitchStrenght", glitchStrenght);
        screenMaterial.SetFloat("_ScanLineStrenght", scanLineStrenght);
    }
}
