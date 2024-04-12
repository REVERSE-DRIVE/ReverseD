using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecoderManager : MonoBehaviour
{
    [SerializeField] private Transform _decoderParent;
    [SerializeField] private Image progressImage;
    [SerializeField] private TMP_Text progressText;
    
    private bool isDecoding;
    private readonly float[] _decodeTime = { 30f, 18f, 6f };

    public void StartDecode(int level)
    {
        if (_decoderParent.childCount < 3 || isDecoding) return;

        StartCoroutine(Decode(level));
    }
    
    private IEnumerator Decode(int index)
    {
        isDecoding = true;
        progressImage.fillAmount = 0f;
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

        for (int i = 0; i < _decoderParent.childCount; i++)
        {
            _decoderParent.GetChild(i).gameObject.SetActive(false);
        }
        isDecoding = false;
    }
}
