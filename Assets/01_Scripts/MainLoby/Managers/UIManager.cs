using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainLoby
{
    public class UIManager : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button _folderButton;
        [SerializeField] private Button _decoderButton;
        [SerializeField] private Button _startButton;
        [Space] 
        
        [Header("Panel")]
        [SerializeField] private Image _playerPanel;
        [SerializeField] private Image _folderPanel;
        [SerializeField] private Image _decoderPanel;
        
        private bool isOpenPlayerPanel;
        private bool isOpenFolderPanel;
        private bool isOpenDecoderPanel;
        private void Start()
        {
            _folderButton.onClick.AddListener(OnFolderButtonClick);
            _decoderButton.onClick.AddListener(OnDecoderButtonClick);
            _startButton.onClick.AddListener(OnStartButtonClick);
        }
        
        private void OnFolderButtonClick()
        {
            if (isOpenFolderPanel) return;
            isOpenFolderPanel = true;
            OpenPanel(_folderPanel, 1.39f);
        }
        
        private void OnDecoderButtonClick()
        {
            if (isOpenDecoderPanel) return;
            isOpenDecoderPanel = true;
            OpenPanel(_decoderPanel, 1.39f);
        }
        
        private void OnStartButtonClick()
        {
            if (isOpenPlayerPanel) return;
            isOpenPlayerPanel = true;
            OpenPanel(_playerPanel, 0.35f);
        }

        /** <summary>
         * 패널 열기
         * </summary>
         * <param name="panel">열 패널</param>
         * <param name="size">사이즈</param>
         */
        private void OpenPanel(Image panel, float size)
        {
            panel.gameObject.SetActive(true);
            
            Sequence sq = DOTween.Sequence();
            sq.Append(panel.rectTransform.DOScale(new Vector3(0.1f, 0.1f), 0f).SetEase(Ease.Linear));
            sq.AppendInterval(0.1f);
            sq.Append(panel.rectTransform.DOScaleY(size, 0.15f).SetEase(Ease.Linear));
            sq.AppendInterval(0.5f);
            sq.Join(panel.rectTransform.DOScaleX(size, 0.15f).SetEase(Ease.Linear));

            sq.Play();
        }
        
    }
}

