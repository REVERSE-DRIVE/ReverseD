using System;
using UnityEngine;

namespace MainLoby
{
    public class DecoderController : MonoBehaviour
    {
        public Action OnDecoding;
        
        [SerializeField] private Transform _decoderParent;
        [SerializeField] private Transform _resultParent;
        
        private DropableUI _decoderDropableUI;
        private bool _isFirst = true;
        private bool  _isDecoding;
        private float _backgroundTime;
        
        public Transform DecoderParent => _decoderParent;
        public Transform ResultParent => _resultParent;
        public float ForegroundTime { get; private set; }

        public float BackgroundTime
        {
            get => _backgroundTime;
            set => _backgroundTime = value;
        }
        public bool IsDecoding
        {
            get => _isDecoding;
            set => _isDecoding = value;
        }

        private void Awake()
        {
            _decoderDropableUI = _decoderParent.GetComponent<DropableUI>();
        }

        private void Update()
        {
            CheckAndStartDecode();
        }

        private void CheckAndStartDecode()
        {
            _decoderParent.GetComponent<DropableUI>().enabled = _decoderParent.childCount < 3;
            if (_decoderParent.childCount == 3)
            {
                OnDecoding?.Invoke();
            }
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (!_isFirst)
            {
                if (pause)
                {
                    ForegroundTime = Time.time;
                    Debug.Log("Foreground Time: " + ForegroundTime);
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
