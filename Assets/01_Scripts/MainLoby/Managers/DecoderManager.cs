using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace MainLoby
{
    public class DecoderManager : MonoBehaviour
    {
        [SerializeField] private Transform _decoderParent;
        [SerializeField] private Image progressImage;
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image _arrowImage;
        [SerializeField] private int _level;
        
        private bool  _isDecoding;
        private bool _isFirst = true;
        private float _startTime;
        private float _endTime;
        private float _foregroundTime;
        private float _backgroundTime;
        private readonly float[] _decodeTime = { 300f, 18f, 6f };
        
        private void Update()
        {
            _decoderParent.GetComponent<DropableUI>().enabled = _decoderParent.childCount < 3;
            if (_decoderParent.childCount == 3)
            {
                StartDecode();
            }
        }

        private IEnumerator Decoding()
        {
            if (!_isDecoding) yield break;
            

            while (_isDecoding)
            {
                if (progressImage.fillAmount >= 1)
                {
                    _isDecoding = false;
                    progressImage.fillAmount = 0;
                    StopCoroutine(nameof(TitleTextAnimation));
                    titleText.text = "Decoding Complete";
                    yield break;
                }
                progressImage.fillAmount = 
                    (Time.time + (_backgroundTime - _foregroundTime) - _startTime) / _decodeTime[_level - 1];
                _foregroundTime = 0;
                progressText.text = $"{progressImage.fillAmount * 100:F0}%";
                
                // arrowImage의 fillAmount가 0에서 1로 증가하고 1이 되면 0으로 돌아감
                _arrowImage.fillAmount += Time.deltaTime;
                if (_arrowImage.fillAmount >= 1)
                {
                    _arrowImage.fillAmount = 0;
                }
                
                yield return null;
            }
        }

        public void StartDecode()
        {
            if (_decoderParent.childCount < 3 ||  _isDecoding) return;
            
            progressImage.fillAmount = 0;
            
            _startTime = Time.time;
            _endTime = _startTime + _decodeTime[_level - 1];
            
             _isDecoding = true;
             
            StartCoroutine(nameof(TitleTextAnimation));
            StartCoroutine(Decoding());
        }
        
        private IEnumerator TitleTextAnimation()
        {
            while ( _isDecoding)
            {
                titleText.text = "Decoding" + new string('.', (int) (Time.time * 2) % 4);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (!_isFirst)
            {
                if (pause)
                {
                    _foregroundTime = Time.time;
                    Debug.Log("Foreground Time: " + _foregroundTime);
                }
                else
                {
                    _backgroundTime = Time.time;
                    Debug.Log("Background Time: " + _backgroundTime);
                }
            }
            else
            {
                _isFirst = false;
            }
        }
    }
}
