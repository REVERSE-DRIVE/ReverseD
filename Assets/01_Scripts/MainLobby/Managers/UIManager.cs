using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

namespace MainLobby
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

        public void OnExitButtonClick()
        {
            Debug.Log("Exit");
            ClosePanel(transform.parent.GetComponent<Image>());
        }

        private void OnFolderButtonClick()
        {
            if (isOpenFolderPanel) return;
            isOpenFolderPanel = true;
            OpenPanel(_folderPanel, 2f);
        }
        
        private void OnDecoderButtonClick()
        {
            if (isOpenDecoderPanel) return;
            isOpenDecoderPanel = true;
            OpenPanel(_decoderPanel, 2f);
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

        /** <summary>
         * 패널 닫기
         * </summary>
         * <param name="panel">닫을 패널</param>
         */
        private void ClosePanel(Image panel)
        {
            Sequence sq = DOTween.Sequence();
            sq.Append(panel.rectTransform.DOScaleY(0f, 0.15f).SetEase(Ease.Linear));
            sq.AppendInterval(0.5f);
            sq.Join(panel.rectTransform.DOScaleX(0f, 0.15f).SetEase(Ease.Linear));

            sq.Play();
        }
        
    }
}

