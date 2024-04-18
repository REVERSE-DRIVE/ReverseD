using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainLoby
{
    public class Decoder : MonoBehaviour
    {
        [SerializeField] private DecoderController _decoderController;
        [SerializeField] private FileItem _filePrefab;
        
        [SerializeField] private Image progressImage;
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image _arrowImage;
        [SerializeField] private int _level;
        
        private float _startTime;
        private float _endTime;
        
        private readonly float[] _decodeTime = { 30f, 18f, 6f };
        
        private void Awake()
        {
            _decoderController.OnDecoding += StartDecode;
        }

        private void OnDestroy()
        {
            _decoderController.OnDecoding -= StartDecode;
        }

        private void StartDecode()
        {
            if (!CanStartDecoding()) return;
            InitializeDecoding();

            StartCoroutine(nameof(TitleTextAnimation));
            StartDecodingCoroutine();
        }
        
        private bool CanStartDecoding()
        {
            return _decoderController.DecoderParent.childCount >= 3 && !_decoderController.IsDecoding;
        }

        private void InitializeDecoding()
        {
            progressImage.fillAmount = 0;

            _startTime = Time.time;
            _endTime = _startTime + _decodeTime[_level - 1];

            _decoderController.IsDecoding = true;
        }

        private void StartDecodingCoroutine()
        {
            StartCoroutine(Decoding());
        }

        private IEnumerator Decoding()
        {
            if (!_decoderController.IsDecoding) yield break;
            DragAndDropEnable(false);
            while (_decoderController.IsDecoding)
            {
                if (progressImage.fillAmount >= 1)
                {
                    ResetDecoding();
                    DestroyFiles();
                    CreateDecodedFile();
                    yield break;
                }
                GaugeFill();
                ArrowAnimation();

                yield return null;
            }
        }
        
        private void ResetDecoding()
        {
            _decoderController.IsDecoding = false;
            progressImage.fillAmount = 0;
            StopCoroutine(nameof(TitleTextAnimation));
            titleText.text = "Decoding Complete";
        }
        
        private void DestroyFiles()
        {
            FileItem[] files = _decoderController.DecoderParent.GetComponentsInChildren<FileItem>();
            foreach (FileItem fileItem in files)
            {
                Destroy(fileItem.gameObject);
            }
        }

        private void CreateDecodedFile()
        {
            FileItem file = Instantiate(_filePrefab, _decoderController.ResultParent);
            file.SetFileName("Decoded File");
        }
        
        private IEnumerator TitleTextAnimation()
        {
            while (_decoderController.IsDecoding)
            {
                titleText.text = "Decoding" + new string('.', (int) (Time.time * 2) % 4);
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        private void GaugeFill()
        {
            progressImage.fillAmount = 
                (Time.time + (_decoderController.BackgroundTime - _decoderController.ForegroundTime) - _startTime) / _decodeTime[_level - 1];
            _decoderController.BackgroundTime = 0;
            progressText.text = $"{progressImage.fillAmount * 100:F0}%";
        }
        
        private void ArrowAnimation()
        {
            _arrowImage.fillAmount += Time.deltaTime;
            if (_arrowImage.fillAmount >= 1)
            {
                _arrowImage.fillAmount = 0;
            }
        }
        
        private void DragAndDropEnable(bool value)
        {
            _decoderController.DecoderParent.GetComponent<DropableUI>().enabled = value;
            DraggableUI[] draggables = _decoderController.DecoderParent.GetComponentsInChildren<DraggableUI>();
            foreach (DraggableUI draggable in draggables)
            {
                draggable.enabled = value;
            }
        }
    }
}