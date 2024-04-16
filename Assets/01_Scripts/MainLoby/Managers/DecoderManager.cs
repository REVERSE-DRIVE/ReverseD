using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecoderManager : MonoBehaviour
{
    [SerializeField] private Transform _decoderParent;
    [SerializeField] private Image progressImage;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text titleText;
    
    private bool isDecoding;
    private readonly float[] _decodeTime = { 30f, 18f, 6f };

    private void Update()
    {
        _decoderParent.GetComponent<DropableUI>().enabled = _decoderParent.childCount < 3;
    }

    public void StartDecode(int level)
    {
        if (_decoderParent.childCount < 3 || isDecoding) return;

        StartCoroutine(Decode(level));
    }
    
    /** <summary>
     * Decoding 처리해주는 코루틴
     * </summary>
     * <param name="index">DecodeTime의 index번호</param>
     */
    private IEnumerator Decode(int index)
    {
        isDecoding = true;
        progressImage.fillAmount = 0f;
        progressImage.color = Color.white;
        progressText.color = Color.white;
        StartCoroutine(nameof(TitleTextAnimation));
        _decoderParent.GetComponent<DropableUI>().enabled = false;
        for (int i = 0; i < _decoderParent.childCount; i++)
        {
            _decoderParent.GetChild(i).GetComponent<DraggableUI>().enabled = false;
        }
        
        // gauge fill
        while (true)
        {
            progressImage.fillAmount += Time.deltaTime / _decodeTime[index];
            progressText.text = $"{progressImage.fillAmount * 100:F0}%";
            if (progressImage.fillAmount >= 1f) break;
            yield return null;
        }
        
        Debug.Log("decode complete");
        progressImage.color = Color.green;
        progressText.color = Color.green;
        StopCoroutine(nameof(TitleTextAnimation));
        titleText.text = "Decoding Complete";
        for (int i = 0; i < _decoderParent.childCount; i++)
        {
            Destroy(_decoderParent.GetChild(i).gameObject);
        }
        _decoderParent.GetComponent<DropableUI>().enabled = true;
        isDecoding = false;
    }
    
    /** <summary>
     * 타이틀 텍스트 애니메이션
     * </summary>
     */
    private IEnumerator TitleTextAnimation()
    {
        while (isDecoding)
        {
            titleText.text = "Decoding";
            yield return new WaitForSeconds(0.5f);
            titleText.text = "Decoding.";
            yield return new WaitForSeconds(0.5f);
            titleText.text = "Decoding..";
            yield return new WaitForSeconds(0.5f);
            titleText.text = "Decoding...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
